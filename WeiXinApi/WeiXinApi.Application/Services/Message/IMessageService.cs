

namespace WeiXinApi.Application.Services
{
    public interface IMessageService
    {

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
