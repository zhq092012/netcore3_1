// ServiceA.cs
// zhanghq--zhq_092012@163.com
// 2020/3/22
using System;
using qf.AspNetCore3_1.Interface;

namespace qf.AspNetCore3_1.Service
{
    public class ServiceA : InterfaceA
    {
        public ServiceA()
        {
        }

        public void show()
        {
            Console.WriteLine("serviceA...");
        }
    }
}
