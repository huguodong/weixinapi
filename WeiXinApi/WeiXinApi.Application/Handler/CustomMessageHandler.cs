
namespace WeiXinApi.Application.Handler
{
    public class CustomMessageHandler : MessageHandler<DefaultMpMessageContext>
    {
        private readonly IMessageService _messageService;
        private string appId = Config.SenparcWeixinSetting.WeixinAppId;
        private string appSecret = Config.SenparcWeixinSetting.WeixinAppSecret;

        public CustomMessageHandler(Stream inputStream,
                                    PostModel postModel,
                                    IMessageService messageService,
                                    int maxRecordCount = 0,
                                    bool onlyAllowEncryptMessage = false,
                                    Senparc.NeuChar.App.AppStore.DeveloperInfo developerInfo = null,
                                    IServiceProvider serviceProvider = null)
            : base(inputStream, postModel, maxRecordCount, onlyAllowEncryptMessage, developerInfo, serviceProvider)
        {
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalGlobalMessageContext.ExpireMinutes = 3。
            GlobalMessageContext.ExpireMinutes = 3;

            OnlyAllowEncryptMessage = false;//是否只允许接收加密消息，默认为 false

            if (!string.IsNullOrEmpty(postModel.AppId))
            {
                appId = postModel.AppId;//通过第三方开放平台发送过来的请求
            }

            //在指定条件下，不使用消息去重
            base.OmitRepeatedMessageFunc = requestMessage =>
            {
                var textRequestMessage = requestMessage as RequestMessageText;
                if (textRequestMessage != null && textRequestMessage.Content == "容错")
                {
                    return false;
                }
                return true;
            };
            this._messageService = messageService;
        }

        public override async Task OnExecutingAsync(CancellationToken cancellationToken)
        {
            //演示：MessageContext.StorageData
            var currentMessageContext = await base.GetUnsafeMessageContext();//为了在分布式缓存下提高读写效率，使用此方法，如果需要获取实时数据，应该使用 base.GetCurrentMessageContext()
            if (currentMessageContext.StorageData == null || !(currentMessageContext.StorageData is int))
            {
                currentMessageContext.StorageData = (int)0;
                //await GlobalMessageContext.UpdateMessageContextAsync(currentMessageContext);//储存到缓存
            }
        }

        public override async Task OnExecutedAsync(CancellationToken cancellationToken)
        {
            //演示：MessageContext.StorageData

            var currentMessageContext = await base.GetUnsafeMessageContext();//为了在分布式缓存下提高读写效率，使用此方法，如果需要获取实时数据，应该使用 base.GetCurrentMessageContext()
            currentMessageContext.StorageData = ((int)currentMessageContext.StorageData) + 1;
            GlobalMessageContext.UpdateMessageContext(currentMessageContext);//储存到缓存
            await base.OnExecutedAsync(cancellationToken);
        }


        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>(); //ResponseMessageText也可以是News等其他类型
            responseMessage.Content = "这条消息来自DefaultResponseMessage。";
            return responseMessage;
        }

        public override async Task<IResponseMessageBase> OnTextRequestAsync(RequestMessageText requestMessage)
        {
            var currentMessageContext = await base.GetCurrentMessageContext();
            //var result = await _messageService.OnTextRequestAsync(requestMessage, currentMessageContext, GlobalMessageContext.ExpireMinutes, GlobalMessageContext.MaxRecordCount);
            var result = await _messageService.OnTextDbRequestAsync(requestMessage);
            return result;
        }
    }
}
