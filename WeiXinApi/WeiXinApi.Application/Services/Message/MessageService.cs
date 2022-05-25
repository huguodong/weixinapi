
using System.Linq;
using WeiXinApi.Core;

namespace WeiXinApi.Application.Services
{
    public class MessageService : IMessageService, ITransient
    {
        public async Task<ResponseMessageText> OnTextRequestAsync(RequestMessageText requestMessage, DefaultMpMessageContext mpMessageContext, int ExpireMinutes, int MaxRecordCount)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            var requestHandler = await requestMessage.StartHandler()
                //关键字不区分大小写，按照顺序匹配成功后将不再运行下面的逻辑
                .Keyword("你好", () =>
                {
                    responseMessage.Content = "你也好啊！";
                    return responseMessage;
                }).Default(async () =>
                {
                    var result = new StringBuilder();
                    result.AppendFormat("您刚才发送了文字信息：{0}\r\n\r\n", requestMessage.Content);

                    var currentMessageContext = mpMessageContext;
                    if (currentMessageContext.RequestMessages.Count > 1)
                    {
                        result.AppendFormat("您此前还发送了如下消息（{0}/{1}）：\r\n", currentMessageContext.RequestMessages.Count,
                            currentMessageContext.StorageData);
                        for (int i = currentMessageContext.RequestMessages.Count - 2; i >= 0; i--)
                        {
                            var historyMessage = currentMessageContext.RequestMessages[i];
                            result.AppendFormat("{0} 【{1}】{2}\r\n",
                                historyMessage.CreateTime.ToString("HH:mm:ss"),
                                historyMessage.MsgType.ToString(),
                                (historyMessage is RequestMessageText)
                                    ? (historyMessage as RequestMessageText).Content
                                    : $"[非文字类型{((historyMessage is IRequestMessageEventKey eventKey) ? $"-{eventKey.EventKey}" : "")}]"
                                );
                        }
                        result.AppendLine("\r\n");
                    }

                    result.AppendFormat("如果您在{0}分钟内连续发送消息，记录将被自动保留（当前设置：最多记录{1}条）。过期后记录将会自动清除。\r\n",
                        ExpireMinutes, MaxRecordCount);
                    result.AppendLine("\r\n");
                    result.AppendLine(
                        "您还可以发送【位置】【图片】【语音】【视频】等类型的信息（注意是这几种类型，不是这几个文字），查看不同格式的回复。\r\nSDK官方地址：https://sdk.weixin.senparc.com");
                    responseMessage.Content = result.ToString();

                    return responseMessage;
                });

            return responseMessage;
        }

        public async Task<ResponseMessageText> OnTextDbRequestAsync(RequestMessageText requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            var receives = await DbContext.Db.Queryable<MessageReceive>().ToListAsync();//获取列表
            var receive = receives.Where(it => it.KeyWords == requestMessage.Content).FirstOrDefault();//查找关键字是否存在
            if (receive != null)
            {
                responseMessage.Content = receive.ReceiveString;
            }
            else
            {
                //如果关键字搜不到，列出关键字
                var result = new StringBuilder();
                result.AppendFormat("听不懂你再说什么,可以试试下面的关键字\r\n\r\n");
                for (int i = 0; i < receives.Count; i++)
                {
                    result.AppendFormat($"{i + 1}:{receives[i].KeyWords}\r\n");
                }
                responseMessage.Content = result.ToString();

            }
            return responseMessage;
        }

    }
}
