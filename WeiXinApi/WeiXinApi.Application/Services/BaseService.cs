namespace WeiXinApi.Application.Services
{
    public class BaseService : IDynamicApiController
    {
        public static readonly string Token = Config.SenparcWeixinSetting.MpSetting.Token;//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = Config.SenparcWeixinSetting.MpSetting.EncodingAESKey;//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = Config.SenparcWeixinSetting.MpSetting.WeixinAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。


    }
}
