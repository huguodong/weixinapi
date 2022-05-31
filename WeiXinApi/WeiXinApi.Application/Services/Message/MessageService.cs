

namespace WeiXinApi.Application.Services
{
    public partial class MessageService : IMessageService, ITransient
    {

        private string appId = Config.SenparcWeixinSetting.WeixinAppId;
        private string appSecret = Config.SenparcWeixinSetting.WeixinAppSecret;

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

        public async Task<IResponseMessageBase> OnReceiveDbRequestAsync(RequestMessageText requestMessage)
        {
            var receives = await DbContext.Db.Queryable<MessageReceive>().ToListAsync();//获取列表
            var receive = receives.Where(it => it.KeyWords == requestMessage.Content).FirstOrDefault();//查找关键字是否存在
            if (receive != null)
            {

                switch (receive.ReceiveType)
                {
                    case ReceiveType.文字:
                        var responseText = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
                        responseText.Content = receive.ReceiveString;
                        return responseText;
                    case ReceiveType.图片:
                        var responseImage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestMessage);
                        responseImage.Articles.Add(new Article()
                        {
                            Title = "欢迎",
                            Description = "这是一张图片消息",
                            PicUrl = receive.ReceiveString,
                            Url = "https://www.cnblogs.com/huguodong/"
                        });
                        return responseImage;
                    default:
                        var responseOther = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
                        responseOther.Content = receive.ReceiveString;
                        return responseOther;
                }
            }
            else
            {
                var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
                //如果关键字搜不到，列出关键字
                var result = new StringBuilder();
                result.AppendFormat("听不懂你再说什么,可以试试下面的关键字\r\n");
                for (int i = 0; i < receives.Count; i++)
                {
                    result.AppendFormat($"{i + 1}:{receives[i].KeyWords}\r\n");
                }
                responseMessage.Content = result.ToString();
                return responseMessage;

            }
        }

        public async Task<IResponseMessageBase> OnReceiveImageRequestAsync(RequestMessageImage requestMessage)
        {
            var responseImage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageImage>(requestMessage);
            responseImage.Image.MediaId = requestMessage.MediaId;
            return responseImage;
        }

        public async Task<IResponseMessageBase> OnReceiveVoiceRequestAsync(RequestMessageVoice requestMessage)
        {

            var recognition = requestMessage.Recognition;//文字识别结果
            Console.WriteLine($"文字识别结果:{recognition}");
            if (string.IsNullOrEmpty(recognition))
            {
                var responseText = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
                responseText.Content = "抱歉,听不清你在说啥!";
                return responseText;
            }
            else
            {
                var responseVoice = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageVoice>(requestMessage);
                responseVoice.Voice.MediaId = requestMessage.MediaId;
                return responseVoice;
            }

        }


        public async Task<IResponseMessageBase> OnReceiveVideoRequestAsync(RequestMessageVideo requestMessage)
        {

            var responseVideo = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageVideo>(requestMessage);

            #region 已丢弃
            //responseVideo.Video.Title = "这是您刚才发送的视频";
            //responseVideo.Video.Description = "这是一条视频消息";
            //responseVideo.Video.MediaId = requestMessage.MediaId;
            //上传素材
            //var dir = ServerUtility.ContentRootMapPath("~/App_Data/TempVideo/");
            //var file = await MediaApi.GetAsync(appId, requestMessage.MediaId, dir);
            //var uploadResult = await MediaApi.UploadTemporaryMediaAsync(appId, UploadMediaFileType.video, file, 5000);
            //responseVideo.Video.Title = "这是您刚才发送的视频";
            //responseVideo.Video.Description = "这是一条视频消息";
            //responseVideo.Video.MediaId = uploadResult.media_id;
            #endregion

            responseVideo.Video.Title = "这是您刚才发送的视频";
            responseVideo.Video.Description = "这是一条视频消息";
            responseVideo.Video.MediaId = "FOA4ilSWolCe4sVJxKHdkpMOhOXy1GgAE_exyCzxFQ-LZ6FJCmUlYpmxqMJKEmCk";
            return responseVideo;

        }

        public async Task<IResponseMessageBase> OnReceiveShortVideoRequestAsync(RequestMessageShortVideo requestMessage)
        {
            var responseText = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseText.Content = "您刚才发送的是小视频";
            return responseText;
        }


        public async Task<IResponseMessageBase> OnReceiveLocationRequestAsync(RequestMessageLocation requestMessage)
        {
            var responseNews = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestMessage);
            var markersList = new List<BaiduMarkers>();
            markersList.Add(new BaiduMarkers()
            {
                Longitude = requestMessage.Location_X,
                Latitude = requestMessage.Location_Y,
                Color = "red",
                Label = "S",
                Size = BaiduMarkerSize.m
            });

            var mapUrl = BaiduMapHelper.GetBaiduStaticMap(requestMessage.Location_X, requestMessage.Location_Y, 1, 6, markersList);
            responseNews.Articles.Add(new Article()
            {
                Description = string.Format("【来自百度地图】您刚才发送了地理位置信息。Location_X：{0}，Location_Y：{1}，Scale：{2}，标签：{3}",
                           requestMessage.Location_X, requestMessage.Location_Y,
                           requestMessage.Scale, requestMessage.Label),
                PicUrl = mapUrl,
                Title = "定位地点周边地图",
                Url = mapUrl
            });
            return responseNews;
        }


        public async Task<IResponseMessageBase> OnReceiveLinkRequestAsync(RequestMessageLink requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestMessage);
            responseMessage.Articles.Add(new Article
            {
                Title = requestMessage.Title,
                Description = requestMessage.Description,
                PicUrl = "https://pic.cnblogs.com/avatar/668465/20210318093258.png",
                Url = requestMessage.Url
            });
            return responseMessage;
        }




    }
}
