// SimpleMiddleWare.cs
// zhanghq--zhq_092012@163.com
// 2020/3/22
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace qf.AspNetCore3_1.Project.MiddleWare
{
  public class SimpleMiddleWare
  {
    private readonly RequestDelegate _next;
    public SimpleMiddleWare(RequestDelegate next)
    {
      _next = next;
    }
    public async Task Invork(HttpContext context)
    {
      Console.WriteLine("invork");
      await _next.Invoke(context);
    }
  }
}
