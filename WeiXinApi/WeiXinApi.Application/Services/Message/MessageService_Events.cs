

namespace WeiXinApi.Application.Services
{
    public partial class MessageService
    {
        public async Task<IResponseMessageBase> OnEvent_SubscribeRequestAsync(RequestMessageEvent_Subscribe requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            if (!string.IsNullOrWhiteSpace(requestMessage.EventKey) && requestMessage.EventKey.StartsWith("qrscene_"))//扫二维码事件
            {
                responseMessage.Content = await GetScanRequest(requestMessage.EventKey.Split("_")[1]);
            }
            else
            {
                var receives = await DbContext.Db.Queryable<MessageReceive>().ToListAsync();
                //如果关键字搜不到，列出关键字
                var result = new StringBuilder();
                result.AppendFormat("欢迎订阅,可以试试下面的关键字\r\n\r\n");
                for (int i = 0; i < receives.Count; i++)
                {
                    result.AppendFormat($"{i + 1}:{receives[i].KeyWords}\r\n");
                }
                responseMessage.Content = result.ToString();
            }
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
            #region 弃用
            //var result = "扫描带二维码事件!";
            //var sceneId = requestMessage.EventKey.ObjToInt();
            //var receive = await DbContext.Db.Queryable<QrCode>().Where(it => it.SceneId == sceneId).FirstAsync();//从数据库拿数据
            //if (receive != null)
            //{
            //    switch (receive.ReceiveInfo.ReceiveType)
            //    {
            //        case ReceiveType.文字:
            //            result += $"{receive.ReceiveInfo.ReceiveString}";
            //            break;
            //    }
            //}
            //responseMessage.Content=result;
            #endregion
            responseMessage.Content = await GetScanRequest(requestMessage.EventKey);
            return responseMessage;
        }

        /// <summary>
        /// 获取扫描事件结果
        /// </summary>
        /// <param name="eventKey"></param>
        /// <returns></returns>
        private async Task<string> GetScanRequest(string eventKey)
        {
            Console.WriteLine($"eventKey:{eventKey}");
            var result = "扫描带二维码事件!";
            var sceneId = eventKey.ObjToInt();
            var receive = await DbContext.Db.Queryable<QrCode>().Where(it => it.SceneId == sceneId).FirstAsync();//从数据库拿数据
            Console.WriteLine($"是否有数据:{receive != null}");
            if (receive != null)
            {
                switch (receive.ReceiveInfo.ReceiveType)
                {
                    case ReceiveType.文字:
                        result += $"{receive.ReceiveInfo.ReceiveString}";
                        break;
                }
            }
            return result;

        }

        public async Task<IResponseMessageBase> OnEvent_LocationRequestAsync(RequestMessageEvent_Location requestMessage)
        {
            //这里是微信客户端（通过微信服务器）自动发送过来的位置信息
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            var result = $"Latitude:{requestMessage.Latitude},Longitude:{requestMessage.Longitude}";
            Console.WriteLine(result);
            responseMessage.Content = result;//这里写什么都无所谓
            return responseMessage;
        }


        public async Task<IResponseMessageBase> OnEvent_ClickRequestAsync(RequestMessageEvent_Click requestMessage)
        {
            Console.WriteLine("进入点击事件");
            IResponseMessageBase reponseMessage = null;
            switch (requestMessage.EventKey)
            {
                case "V1001_TODAY_MUSIC":
                    {
                        var filePath = "~/wwwroot/Images/music.jpeg";
                        var uploadResult = await MediaApi.UploadTemporaryMediaAsync(appId, UploadMediaFileType.thumb,
                                                                        ServerUtility.ContentRootMapPath(filePath));

                        //PS：缩略图官方没有特别提示文件大小限制，实际测试哪怕114K也会返回文件过大的错误，因此尽量控制在小一点
                        //设置音乐信息
                        var strongResponseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageMusic>(requestMessage);
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Music.Title = "天籁之音";
                        strongResponseMessage.Music.Description = "真的是天籁之音";
                        strongResponseMessage.Music.MusicUrl = "https://sdk.weixin.senparc.com/Content/music1.mp3";
                        strongResponseMessage.Music.HQMusicUrl = "https://sdk.weixin.senparc.com/Content/music1.mp3";
                        strongResponseMessage.Music.ThumbMediaId = uploadResult.thumb_media_id;
                    }
                    break;

                default:
                    {
                        var strongResponseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
                        strongResponseMessage.Content = "您点击了按钮，EventKey：" + requestMessage.EventKey;
                        reponseMessage = strongResponseMessage;
                    }
                    break;
            }

            return reponseMessage;
        }

        public async Task<IResponseMessageBase> OnEvent_ViewRequestAsync(RequestMessageEvent_View requestMessage)
        {
            Console.WriteLine("进入链接跳转事件");
            //说明：这条消息只作为接收，下面的responseMessage到达不了客户端，类似OnEvent_UnsubscribeRequest
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = "您点击了view按钮，将打开网页：" + requestMessage.EventKey;
            return responseMessage;
        }

        public async Task OnEvent_TemplateSendJobFinishRequestAsync(RequestMessageEvent_TemplateSendJobFinish requestMessage)
        {
            Console.WriteLine("进入模板事件推送");

            Console.WriteLine($"发送结果:{requestMessage.Status}");
        }
    }
}
