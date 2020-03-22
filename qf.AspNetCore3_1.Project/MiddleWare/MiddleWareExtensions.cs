// MiddleWareExtensions.cs
// zhanghq--zhq_092012@163.com
// 2020/3/22
using System;
using Microsoft.AspNetCore.Builder;

namespace qf.AspNetCore3_1.Project.MiddleWare
{
  public static class MiddleWareExtensions
  {
    public static IApplicationBuilder SimpleMiddleWare(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<SimpleMiddleWare>();
    }
  }
}
