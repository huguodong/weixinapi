namespace WeiXinApi.Application.Services
{
    public class TemplateInput
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 小程序信息
        /// </summary>
        public TemplateModel_MiniProgram MiniProgram { get; set; }

        /// <summary>
        /// 模板数据
        /// </summary>
        public TemplateData Data { get; set; }
    }

    public class TemplateData
    {

        /// <summary>
        /// 跳转链接
        /// </summary>
        public TemplateDataInfo first { get; set; }


        /// <summary>
        /// 跳转链接
        /// </summary>
        public TemplateDataInfo keyword1 { get; set; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public TemplateDataInfo keyword2 { get; set; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public TemplateDataInfo keyword3 { get; set; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public TemplateDataInfo remark { get; set; }


    }

    public class TemplateDataInfo
    {
        /// <summary>
        /// 值
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string color { get; set; } = "#173177";
    }

}
