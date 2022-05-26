using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Senparc.CO2NET;
using Senparc.CO2NET.AspNet;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.RegisterServices;
using System.IO;

namespace WeiXinApi.Web.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //如果部署在linux系统上，需要加上下面的配置：
        services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
        //如果部署在IIS上，需要加上下面的配置：
        //services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);

        services.AddJwt<JwtHandler>();

        services.AddCorsAccessor();

        services.AddControllers()
                .AddInject();

        services.AddMemoryCache();//使用本地缓存必须添加
        var config = App.Configuration;
        services.AddSenparcGlobalServices(config)//Senparc.CO2NET 全局注册
                 .AddSenparcWeixinServices(config);//Senparc.Weixin 注册
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        //app.UseHttpsRedirection();

        app.UseRouting();

        var senparcSetting = App.GetOptions<SenparcSetting>();
        var senparcWeixinSetting = App.GetOptions<SenparcWeixinSetting>();

        // 启动 CO2NET 全局注册，必须！
        var registerService = app.UseSenparcGlobal(env, senparcSetting, globalRegister =>
         {
             globalRegister.RegisterTraceLog(ConfigTraceLog);//配置TraceLog

         }, true)
         .UseSenparcWeixin(senparcWeixinSetting, (weixinRegister, weixinSetting) =>
         {
             AccessTokenContainer.RegisterAsync(senparcWeixinSetting.MpSetting.WeixinAppId, senparcWeixinSetting.MpSetting.WeixinAppSecret).Wait();
         });
        app.UseCorsAccessor();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseInject(string.Empty);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
    /// <summary>
    /// 配置微信跟踪日志（演示，按需）
    /// </summary>
    private void ConfigTraceLog()
    {
        //这里设为Debug状态时，/App_Data/WeixinTraceLog/目录下会生成日志文件记录所有的API请求日志，正式发布版本建议关闭

        //如果全局的IsDebug（Senparc.CO2NET.Config.IsDebug）为false，此处可以单独设置true，否则自动为true
        Senparc.CO2NET.Trace.SenparcTrace.SendCustomLog("系统日志", "系统启动");//只在Senparc.Weixin.Config.IsDebug = true的情况下生效

        //全局自定义日志记录回调
        Senparc.CO2NET.Trace.SenparcTrace.OnLogFunc = () =>
        {
            //加入每次触发Log后需要执行的代码
            System.Console.WriteLine("加入每次触发Log后需要执行的代码");
        };

        //当发生基于WeixinException的异常时触发
        WeixinTrace.OnWeixinExceptionFunc = async ex =>
        {
            //加入每次触发WeixinExceptionLog后需要执行的代码
            System.Console.WriteLine("异常了======================");
            System.Console.WriteLine(ex.Message);
            System.Console.WriteLine(ex.InnerException);
            System.Console.WriteLine("======================");
        };
    }
}
