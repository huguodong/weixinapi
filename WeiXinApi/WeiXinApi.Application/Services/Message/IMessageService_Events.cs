namespace WeiXinApi.Application.Services
{
    public partial interface IMessageService
    {
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        Task<IResponseMessageBase> OnEvent_SubscribeRequestAsync(RequestMessageEvent_Subscribe requestMessage);

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        Task<IResponseMessageBase> OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage);


        /// <summary>
        /// 扫描带参数二维码事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        Task<IResponseMessageBase> OnEvent_ScanRequestAsync(RequestMessageEvent_Scan requestMessage);


    }
}
