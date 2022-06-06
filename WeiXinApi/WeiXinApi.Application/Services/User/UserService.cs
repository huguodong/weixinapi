using Furion.RemoteRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinApi.Application.Services.User
{
    public class UserService : BaseService
    {

        /// <summary>
        /// 创建标签
        /// </summary>
        /// <returns></returns>
        [HttpPost("/user/tag/add")]
        public async Task<dynamic> CreateTag(TagInput input)
        {
            return await UserTagApi.CreateAsync(AppId, input.Name);
        }

        /// <summary>
        /// 获取公众号已创建的标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/user/tag/list")]
        public async Task<dynamic> GetTags(TagInput input)
        {
            return await UserTagApi.GetAsync(AppId);
        }

        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/user/tag/update")]
        public async Task<dynamic> UpdateTag(UpdateTagInput input)
        {
            return await UserTagApi.UpdateAsync(AppId, input.Id, input.Name);
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/user/tag/delete")]
        public async Task<dynamic> DeleteTag(UpdateTagInput input)
        {
            return await UserTagApi.DeleteAsync(AppId, input.Id);
        }


        /// <summary>
        /// 获取标签下粉丝列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/user/tag/user")]
        public async Task<dynamic> GetTagUser(UpdateTagInput input)
        {
            return await UserTagApi.GetAsync(AppId, input.Id);
        }


        /// <summary>
        /// 批量为用户打标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/user/tag/batch")]
        public async Task<dynamic> BatchTag(UserTagInput input)
        {
            return await UserTagApi.BatchTaggingAsync(AppId, input.Id, input.Users);
        }



        /// <summary>
        /// 批量为用户取消标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/user/tag/unbatch")]
        public async Task<dynamic> BatchUnTag(UserTagInput input)
        {
            return await UserTagApi.BatchUntaggingAsync(AppId, input.Id, input.Users);
        }


        /// <summary>
        /// 获取用户身上的标签列表
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet("/user/tag/batchlist/{openid}")]
        public async Task<dynamic> UserTagList(string openid)
        {
            return await UserTagApi.UserTagListAsync(AppId, openid);
        }

        /// <summary>
        /// 设置用户备注名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/user/updateremark")]
        public async Task<dynamic> UpdateRemark(UserInput input)
        {
            return await UserApi.UpdateRemarkAsync(AppId, input.OpenId, input.Remark);
        }


        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet("/user/info/{openid}")]
        public async Task<dynamic> UserInfo(string openid)
        {
            return await UserApi.InfoAsync(AppId, openid);
        }


        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/user/list")]
        public async Task<dynamic> UserList([FromQuery] UserListInput input)
        {
            return await UserApi.GetAsync(AppId, input.NextOpenId);
        }


        /// <summary>
        /// 批量拉黑用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/user/batchblacklist")]
        public async Task<dynamic> Batchblacklist(BlackInput input)
        {
            return await UserApi.BatchBlackListAsync(AppId, input.OpenidList);
        }

        /// <summary>
        /// 获取黑名单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/user/blacklist")]
        public async Task<dynamic> GetBlackList([FromQuery] BlackInput input)
        {
            return await UserApi.GetBlackListAsync(AppId, input.BeginOpenid);
        }

        /// <summary>
        /// 批量取消拉黑用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/user/batchunblackList")]
        public async Task<dynamic> BatchUnBlackList(BlackInput input)
        {
            return await UserApi.BatchUnBlackListAsync(AppId, input.OpenidList);
        }

    }
}
