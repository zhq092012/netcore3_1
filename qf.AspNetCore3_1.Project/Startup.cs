using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using qf.AspNetCore3_1.Interface;
using qf.AspNetCore3_1.Project.MiddleWare;
using qf.AspNetCore3_1.Service;

namespace qf.AspNetCore3_1.Project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSession();
            services.AddTransient<InterfaceA, ServiceA>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            #region 自定义中间件
            //直接截断
            //app.Run(c => c.Response.WriteAsync("hello world."));
            //Func<RequestDelegate, RequestDelegate> func = new Func<RequestDelegate, RequestDelegate>(
            //   rd =>
            //   {
            //     //return new RequestDelegate(Do);
            //     //return new RequestDelegate(Do1);
            //     //return new RequestDelegate(async context => await Task.Run(() => context.Response.WriteAsync("hello world.")));
            //     //return new RequestDelegate(async context => await Task.Run(() => context.Response.WriteAsync("hello world.")));
            //     //return async context => await Task.Run(() => context.Response.WriteAsync("hello world."));
            //     return context => context.Response.WriteAsync("hello world.");
            //   }
            //  );
            //app.Use(rd => { return context => context.Response.WriteAsync("hello world."); });
            //app.Use(rd => context => context.Response.WriteAsync("hello world."));
            //app.SimpleMiddleWare();

            //app.Use(next =>
            //{
            //  return async c =>
            // {

            //   await c.Response.WriteAsync("this is middleware 2 Start");
            //   await next.Invoke(c);
            //   await c.Response.WriteAsync("this is middleware 2 End");
            // };
            //});
            #endregion

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
            //启用session
            app.UseSession();

            #region 注册log4写日志
            //注册log4写日志
            loggerFactory.AddLog4Net();

            loggerFactory.CreateLogger<Program>().LogWarning("this is config");
            #endregion


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        #region 私有方法
        //private Task Do(HttpContext context)
        //{
        //  return Task.Run(() => { context.Response.WriteAsync("hello world."); });
        //}
        //private async Task Do1(HttpContext context)
        //{
        //  await Task.Run(() => context.Response.WriteAsync("hello world."));
        //}
        #endregion

    }
}
