
namespace WeiXinApi.Application.Services
{
    public class UserInput
    {
        /// <summary>
        /// 微信号
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    public class UserListInput
    {

        /// <summary>
        /// 第一个拉取的OPENID，不填默认从头开始拉取
        /// </summary>
        public string NextOpenId { get; set; }
    }


    public class BlackInput
    {
        /// <summary>
        /// 需要拉入黑名单的用户的openid，一次拉黑最多允许20个
        /// </summary>
        public List<string> OpenidList { get; set; }


        /// <summary>
        /// 当 begin_openid 为空时，默认从开头拉取。
        /// </summary>
        public string BeginOpenid { get; set; }
    }

}
