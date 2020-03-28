using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace qf.AspNetCore3_1.Project
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            //将默认ServiceProviderFactory指定为AutofacServiceProviderFactory
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            //可以在这里配置log4或者在startup中配置log4
            //.ConfigureLogging((context, loggingBuilder) =>
            //{
            //  loggingBuilder.AddFilter("System", LogLevel.Warning);//过滤掉命名空间
            //  loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
            //  loggingBuilder.AddLog4Net();
            //})
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
