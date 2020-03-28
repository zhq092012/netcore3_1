using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using log4net.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using qf.AspNetCore3_1.Interface;
using qf.AspNetCore3_1.Project.Controllers;
using qf.AspNetCore3_1.Project.MiddleWare;
using qf.AspNetCore3_1.Project.Utility;
using qf.AspNetCore3_1.Service;

namespace qf.AspNetCore3_1.Project
{
  public class Startup
  {

    public IConfiguration Configuration { get; }


    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      //全局注册异常处理
      services.AddControllersWithViews(
          options => options.Filters.Add(typeof(CustomExceptionFilterAttribute))
          );
      //全局的特性的依赖注入
      //services.AddScoped<CustomExceptionFilterAttribute>();
      services.AddMemoryCache();//使用本地缓存必须添加
      services.AddSession();
      services.AddSignalR();//使用 SignalR

    }
    #region 注册autofac
    public void ConfigureContainer(ContainerBuilder builder)
    {
      //业务逻辑层所在程序集命名空间
      Assembly service = Assembly.Load("qf.AspNetCore3_1.Service");
      //接口层所在程序集命名空间
      Assembly repository = Assembly.Load("qf.AspNetCore3_1.Interface");
      //自动注入
      builder.RegisterAssemblyTypes(service, repository)
          .Where(t => t.Name.StartsWith("Service"))
          .AsImplementedInterfaces();

    }
    #endregion

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      #region 注册log4写日志
      //注册log4写日志
      loggerFactory.AddLog4Net();
      #endregion

      app.UseHttpsRedirection();
      //启用session
      app.UseSession();

      app.UseStaticFiles();

      app.UseCookiePolicy();

      app.UseRouting();

      app.UseCors("default");//跨域管道

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
      });

    }


  }
}

