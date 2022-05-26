

using WeiXinApi.Core;

namespace WeiXinApi.Application.Services
{
    public class WeiXinService : BaseService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMessageService _messageService;

        public WeiXinService(IHttpContextAccessor​ httpContextAccessor, IMessageService messageService)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._messageService = messageService;
        }

        [HttpGet("/wx")]
        public string Index([FromQuery] PostModel postModel, string echostr)
        {
            //var data = DbContext.Db.Queryable<MessageReceive>().ToList();
            //System.Console.WriteLine(echostr);
            //return echostr;

            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                Console.WriteLine("通过:" + echostr);
                return echostr; //返回随机字符串则表示验证通过
            }
            else
            {
                return ("failed:" + postModel.Signature + "," + Senparc.Weixin.MP.CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, Token) + "。" +
                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

        [HttpPost("/wx")]
        public async Task<ContentResult> Receive([FromQuery] PostModel postModel)
        {
            string filePath = App.WebHostEnvironment.ContentRootPath;
            ContentResult content = new ContentResult();
            Console.WriteLine("收到消息");
            //测试时候忽略验证
            //if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            //{
            //    content.Content = "参数错误！";
            //    return content;
            //}

            postModel.Token = Token;
            postModel.EncodingAESKey = EncodingAESKey;//根据自己后台的设置保持一致
            postModel.AppId = AppId;//根据自己后台的设置保持一致

            //v4.2.2之后的版本，可以设置每个人上下文消息储存的最大数量，防止内存占用过多，如果该参数小于等于0，则不限制（实际最大限制 99999）
            //注意：如果使用分布式缓存，不建议此值设置过大，如果需要储存历史信息，请使用数据库储存
            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            try
            {
                var maxRecordCount = 10;
                var messageHandler = new CustomMessageHandler(_httpContextAccessor.HttpContext.Request.Body, postModel, _messageService, maxRecordCount);

                messageHandler.SaveRequestMessageLog(filePath);//记录 Request 日志（可选）
                var ct = new CancellationToken();
                await messageHandler.ExecuteAsync(ct);//执行微信处理过程（关键）
                messageHandler.SaveResponseMessageLog(filePath);//记录 Response 日志（可选）
                var result = messageHandler.ResponseDocument.ToString();
                content.Content = result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                content.Content = "";
            }

            return content;
        }

    }
}
