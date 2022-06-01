namespace WeiXinApi.Application.Services
{
    public class AddPersonalInput
    {
        /// <summary>
        /// 菜单
        /// </summary>
        public GetMenuResultFull ResultFull { get; set; }

        /// <summary>
        /// 规则
        /// </summary>
        public MenuMatchRule MenuMatchRule { get; set; }
    }
}
