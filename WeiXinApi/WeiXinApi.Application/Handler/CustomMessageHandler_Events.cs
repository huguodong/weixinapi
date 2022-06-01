namespace WeiXinApi.Application.Handler
{
    public partial class CustomMessageHandler
    {

        /// <summary>
        /// 订阅或关注事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnEvent_SubscribeRequestAsync(RequestMessageEvent_Subscribe requestMessage)
        {
            var result = await _messageService.OnEvent_SubscribeRequestAsync(requestMessage);
            return result;
        }

        /// <summary>
        /// 退订
        /// unsubscribe事件的意义在于及时删除网站应用中已经记录的OpenID绑定，消除冗余数据。并且关注用户流失的情况。
        /// </summary>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnEvent_UnsubscribeRequestAsync(RequestMessageEvent_Unsubscribe requestMessage)
        {
            var result = await _messageService.OnEvent_UnsubscribeRequest(requestMessage);
            return result;
        }

        /// <summary>
        /// 扫描带参数二维码事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnEvent_ScanRequestAsync(RequestMessageEvent_Scan requestMessage)
        {
            var result = await _messageService.OnEvent_ScanRequestAsync(requestMessage);
            return result;
        }


        /// <summary>
        /// 位置事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnEvent_LocationRequestAsync(RequestMessageEvent_Location requestMessage)
        {
            var result = await _messageService.OnEvent_LocationRequestAsync(requestMessage);
            return result;
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnEvent_ClickRequestAsync(RequestMessageEvent_Click requestMessage)
        {
            var result = await _messageService.OnEvent_ClickRequestAsync(requestMessage);
            return result;
        }

        /// <summary>
        /// 打开网页事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnEvent_ViewRequestAsync(RequestMessageEvent_View requestMessage)
        {
            var result = await _messageService.OnEvent_ViewRequestAsync(requestMessage);
            return result;
        }

        /// <summary>
        /// 模板发送事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnEvent_TemplateSendJobFinishRequestAsync(RequestMessageEvent_TemplateSendJobFinish requestMessage)
        {
            await _messageService.OnEvent_TemplateSendJobFinishRequestAsync(requestMessage);
            return await base.OnEvent_TemplateSendJobFinishRequestAsync(requestMessage);
        }
    }
}
