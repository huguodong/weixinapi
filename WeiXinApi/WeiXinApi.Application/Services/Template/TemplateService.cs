using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;

namespace WeiXinApi.Application.Services
{
    public class TemplateService : BaseService
    {

        /// <summary>
        /// 设置行业Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/template/setindustry")]
        public async Task<dynamic> SetIndustry(IndustryInput input)
        {
            return await TemplateApi.SetIndustryAsync(AppId, input.Id1, input.Id2);
        }


        /// <summary>
        /// 获取行业信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("/template/industry")]
        public async Task<dynamic> GetIndustry()
        {
            return await TemplateApi.GetIndustryAsync(AppId);
        }

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("/template/list")]
        public async Task<dynamic> GetTemplate()
        {
            return await TemplateApi.GetPrivateTemplateAsync(AppId);
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="template_id"></param>
        /// <returns></returns>
        [HttpPost("/template/delete/{template_id}")]
        public async Task<dynamic> Delete(string template_id)
        {
            return await TemplateApi.DelPrivateTemplateAsync(AppId, template_id);
        }


        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/template/send")]
        public async Task<dynamic> Send(TemplateInput input)
        {
            //var templateId = "7tN5LJReW2HDs8KA4Oiro4IgY1UMPABl2Vr7h3576cs";
            //var openId = "osCZQ6Yvc1BtBRLC53Z4FpKbRKpc";

            //var testData = new //TestTemplateData()
            //{
            //    first = new TemplateDataItem("恭喜你购买成功"),
            //    keyword1 = new TemplateDataItem("巧克力"),
            //    keyword2 = new TemplateDataItem("39.8元"),
            //    keyword3 = new TemplateDataItem(SystemTime.Now.ToString("O")),
            //    remark = new TemplateDataItem("欢迎再次购买！")
            //};
            //var result = await TemplateApi.SendTemplateMessageAsync(AppId, openId, templateId, "www.baidu.com", testData);
            var result = await TemplateApi.SendTemplateMessageAsync(AppId, input.OpenId, input.TemplateId, input.Url, input.Data, input.MiniProgram);
            return result;
        }




    }
}
