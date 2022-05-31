namespace WeiXinApi.Application.Services
{
    public partial class MessageService
    {
        public async Task<IResponseMessageBase> OnEvent_SubscribeRequestAsync(RequestMessageEvent_Subscribe requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);

            var receives = await DbContext.Db.Queryable<MessageReceive>().ToListAsync();
            //如果关键字搜不到，列出关键字
            var result = new StringBuilder();
            result.AppendFormat("欢迎订阅,可以试试下面的关键字\r\n\r\n");
            for (int i = 0; i < receives.Count; i++)
            {
                result.AppendFormat($"{i + 1}:{receives[i].KeyWords}\r\n");
            }
            responseMessage.Content = result.ToString();
            return responseMessage;
        }



        public async Task<IResponseMessageBase> OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            //实际上用户无法收到非订阅账号的消息，所以这里可以随便写。
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = $"再见!";
            Console.WriteLine($"{requestMessage.FromUserName}取消了订阅");
            return responseMessage;
        }


        public async Task<IResponseMessageBase> OnEvent_ScanRequestAsync(RequestMessageEvent_Scan requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            var result = "";
            if (requestMessage.Event == Event.subscribe)
            {
                result += $"用户未关注时，进行关注后的事件推送!";
            }
            else
            {
                result += $"用户已关注时的事件推送!";
            }
            result += $"EventKey:{requestMessage.EventKey},Ticket:{requestMessage.Ticket}";
            responseMessage.Content = result;
            return responseMessage;
        }
    }
}
