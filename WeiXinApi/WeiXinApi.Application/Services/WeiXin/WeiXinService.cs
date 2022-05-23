using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using System;

namespace WeiXinApi.Application.Services.WeiXin
{
    public class WeiXinService : IDynamicApiController
    {
        public static readonly string Token = Config.SenparcWeixinSetting.MpSetting.Token;//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = Config.SenparcWeixinSetting.MpSetting.EncodingAESKey;//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = Config.SenparcWeixinSetting.MpSetting.WeixinAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。


        [HttpGet("/wx")]
        public string Index([FromQuery] PostModel postModel, string echostr)
        {
            //System.Console.WriteLine(echostr);
            //return echostr;

            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                Console.WriteLine("通过:" + echostr);
                return echostr; //返回随机字符串则表示验证通过
            }
            else
            {
                return ("failed:" + postModel.Signature + "," + Senparc.Weixin.MP.CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, Token) + "。" +
                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

    }
}
