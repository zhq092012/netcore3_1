using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using qf.AspNetCore3_1.Interface;
using qf.AspNetCore3_1.Project.Utility;

namespace qf.AspNetCore3_1.Project.Controllers
{

  /// <summary>
  /// 声明filer
  /// </summary>
  /// 特性的依赖注入
  /// 方法1:servicefilter+startup
  /// 方法2:typefilter 使用工厂依赖注入 不需要在startup中注入
  /// 方法3:IfilterFactory 比较少用
  /// 方法4:全局注册
  /// 其他的还有 AuthorizationFilter ResourceFilter  actionFilter  resultFilter 包括ExceptionFilter只能对action起作用
  //[TypeFilter(typeof(CustomExceptionFilterAttribute))]
  //[ServiceFilter(typeof(CustomExceptionFilterAttribute))]
  public class FifthController : Controller
  {
    private readonly ILogger<ThirdController> _logger;
    private readonly IConfiguration _configuration;
    private readonly InterfaceD _interfaceD;

    public FifthController(ILogger<ThirdController> logger, IConfiguration configuration, InterfaceD interfaceD)
    {
      _logger = logger;
      _configuration = configuration;
      _interfaceD = interfaceD;
    }

    //[CustomExceptionFilter]
    public IActionResult Index()
    {
      throw new Exception("new excetion");
    }
    //[CustomExceptionFilter]
    public IActionResult Info()
    {
      throw new Exception("new excetion Info");
    }
    /// <summary>
    /// 资源的aop，标记必须继承Attribute
    /// </summary>
    /// <returns></returns>
    [CustomResourceFilter]
    public IActionResult Show()
    {
      this._interfaceD.Show(1, "this is autofac show");
      base.ViewBag.DateNow = DateTime.Now;
      return View();
    }

  }
}