

using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinApi.Application.Services
{
    public class MenuService : BaseService
    {

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/menu/add")]
        public dynamic CrateMenu(AddMenuInput input)
        {
            try
            {
                //var resultFull = input.Adapt<GetMenuResultFull>();
                //var buttonGroup = CommonApi.GetMenuFromJsonResult(resultFull, new ButtonGroup()).menu;
                //var result = CommonApi.CreateMenu(AppId, buttonGroup);
                //if (result.errmsg == "ok")
                //{
                //    return "菜单更新成功";
                //}
                //else
                //{
                //    return result;
                //}
                return "1";
            }
            catch (Exception ex)
            {

                throw Oops.Oh($"更新失败:{ex.Message}");
            }

        }

        [HttpPost("/menu/test")]
        public dynamic CrateMenu2(Class1 a)
        {
            var b = a.Adapt<GetMenuResultFull>();
            try
            {
                //var resultFull = input.Adapt<GetMenuResultFull>();
                //var buttonGroup = CommonApi.GetMenuFromJsonResult(resultFull, new ButtonGroup()).menu;
                //var result = CommonApi.CreateMenu(AppId, buttonGroup);
                //if (result.errmsg == "ok")
                //{
                //    return "菜单更新成功";
                //}
                //else
                //{
                //    return result;
                //}
                return "1";
            }
            catch (Exception ex)
            {

                throw Oops.Oh($"更新失败:{ex.Message}");
            }

        }
    }
}
