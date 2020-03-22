using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using qf.AspNetCore3_1.Interface;
using qf.AspNetCore3_1.Service;

namespace qf.AspNetCore3_1.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IServiceCollection services = new ServiceCollection();
                services.AddTransient<InterfaceA, ServiceA>();
                services.AddSingleton<InterfaceB, ServiceB>();//进程单例
                services.AddScoped<InterfaceC, ServiceC>();//作用域单例
                var container = services.BuildServiceProvider();
                {
                    System.Console.WriteLine("-----------------------1-----------");
                    var A1 = container.GetService<InterfaceA>();
                    var A2 = container.GetService<InterfaceA>();
                    System.Console.WriteLine(A1.Equals(A2)); System.Console.WriteLine("-----------------------2-----------");
                    var B1 = container.GetService<InterfaceB>();
                    var B2 = container.GetService<InterfaceB>();
                    System.Console.WriteLine(B1.Equals(B2)); System.Console.WriteLine("-----------------------3-----------");
                    var C1 = container.GetService<InterfaceC>();
                    var C2 = container.GetService<InterfaceC>();
                    var C3 = container.CreateScope().ServiceProvider.GetService<InterfaceC>();
                    var C4 = container.CreateScope().ServiceProvider.GetService<InterfaceC>();
                    InterfaceC C5 = null, C6 = null, C7 = null;
                    Task.Run(() =>
                    {
                        C5 = container.GetService<InterfaceC>();
                    });
                    Task.Run(() =>
                    {
                        C6 = container.GetService<InterfaceC>();
                    }).ContinueWith(t =>
                    {
                        C7 = container.GetService<InterfaceC>();
                    });
                    //Thread.Sleep(2000);
                    System.Console.WriteLine("-----------------------aaa-----------");
                    //System.Console.WriteLine(C1.Equals(C2));
                    //System.Console.WriteLine(C1.Equals(C3));
                    //System.Console.WriteLine(C1.Equals(C4));
                    //System.Console.WriteLine(C2.Equals(C4));
                    //System.Console.WriteLine(C3.Equals(C5));
                    //System.Console.WriteLine(C3.Equals(C6));
                    //System.Console.WriteLine(C3.Equals(C7));
                    System.Console.WriteLine(C5.Equals(C6));
                    System.Console.WriteLine(C5.Equals(C7));
                    System.Console.WriteLine(C6.Equals(C7));
                    System.Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }
    }
}
