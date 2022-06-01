

namespace WeiXinApi.Application.Services
{
    public partial interface IMessageService
    {
        


        /// <summary>
        /// 处理文字消息,包括回复图片
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        Task<IResponseMessageBase> OnReceiveDbRequestAsync(RequestMessageText requestMessage);

        /// <summary>
        /// 处理图片
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        Task<IResponseMessageBase> OnReceiveImageRequestAsync(RequestMessageImage requestMessage);

        /// <summary>
        /// 处理链接消息
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        Task<IResponseMessageBase> OnReceiveLinkRequestAsync(RequestMessageLink requestMessage);

        /// <summary>
        /// 处理位置信息
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        Task<IResponseMessageBase> OnReceiveLocationRequestAsync(RequestMessageLocation requestMessage);

        /// <summary>
        /// 处理小视频消息
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        Task<IResponseMessageBase> OnReceiveShortVideoRequestAsync(RequestMessageShortVideo requestMessage);

        /// <summary>
        /// 处理视频消息
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        Task<IResponseMessageBase> OnReceiveVideoRequestAsync(RequestMessageVideo requestMessage);

        /// <summary>
        /// 处理语音消息
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        Task<IResponseMessageBase> OnReceiveVoiceRequestAsync(RequestMessageVoice requestMessage);

        /// <summary>
        /// 从数据库处理文字消息
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        Task<ResponseMessageText> OnTextDbRequestAsync(RequestMessageText requestMessage);

        /// <summary>
        /// 处理文字消息
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="mpMessageContext"></param>
        /// <param name="ExpireMinutes"></param>
        /// <param name="MaxRecordCount"></param>
        /// <returns></returns>
        Task<ResponseMessageText> OnTextRequestAsync(RequestMessageText requestMessage, DefaultMpMessageContext mpMessageContext, int ExpireMinutes, int MaxRecordCount);
    }
}
