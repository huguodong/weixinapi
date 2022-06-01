
using Mapster;
using Newtonsoft.Json;
using Senparc.CO2NET.Extensions;
using StackExchange.Profiling.Internal;

namespace WeiXinApi.Application.Services
{
    public class MenuService : BaseService
    {

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="resultFull"></param>
        /// <returns></returns>
        [HttpPost("/menu/add")]
        public dynamic CrateMenu(GetMenuResultFull resultFull)
        {
            try
            {
                var buttonGroup = CommonApi.GetMenuFromJsonResult(resultFull, new ButtonGroup()).menu;
                var result = CommonApi.CreateMenu(AppId, buttonGroup);
                if (result.errmsg == "ok")
                {
                    return "菜单更新成功";
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw Oops.Oh($"更新失败:{ex.Message}");
            }

        }


        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet("/menu/list")]
        public dynamic GetMenu()
        {
            try
            {
                var result = CommonApi.GetMenu(AppId);
                if (result == null)
                {
                    throw Oops.Oh($"菜单不存在或验证失败");
                }
                else
                {
                    //正常序列化只返回菜单Name，因为GetMenuResult里面button只有name字段，所以这里需要转换一下
                    var menu = JsonConvert.DeserializeObject<GetMenuResultFull>(result.ToJson());
                    return menu;
                }
            }
            catch (Exception ex)
            {
                throw Oops.Oh($"菜单不存在或验证失败:{ex.Message}");

            }
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet("/menu/delete")]
        public dynamic Delete()
        {
            try
            {
                var result = CommonApi.DeleteMenu(AppId);
                if (result.errmsg == "ok")
                {
                    return "删除菜单成功";
                }
                else
                {
                    return result.errmsg;
                }
            }
            catch (Exception ex)
            {
                throw Oops.Oh($"删除菜单失败:{ex.Message}");

            }

        }


        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="resultFull"></param>
        /// <param name="menuMatchRule"></param>
        /// <returns></returns>
        //[HttpPost("/menu/add")]
        //public dynamic CratePersonalMenu(GetMenuResultFull resultFull, MenuMatchRule menuMatchRule)
        //{
        //    try
        //    {
        //        var buttonGroup = CommonApi.GetMenuFromJsonResult(resultFull, new ButtonGroup()).menu;
        //        var result = CommonApi.CreateMenu(AppId, buttonGroup);
        //        if (result.errmsg == "ok")
        //        {
        //            return "菜单更新成功";
        //        }
        //        else
        //        {
        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw Oops.Oh($"更新失败:{ex.Message}");
        //    }

        //}


    }
}
