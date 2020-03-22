using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using qf.AspNetCore3_1.Interface;
using qf.AspNetCore3_1.Service;

namespace qf.AspNetCore3_1.Project.Controllers
{
  public class ThirdController : Controller
  {
    private readonly ILogger<ThirdController> _logger;
    private readonly IConfiguration _configuration;
    private readonly InterfaceA _interfaceA;

    public ThirdController(ILogger<ThirdController> logger, IConfiguration configuration,InterfaceA interfaceA)
    {
      _logger = logger;
      _configuration = configuration;
      _interfaceA=interfaceA;
    }
    public IActionResult Index()
    {
      // InterfaceA interfaceA=new ServiceA();
      // interfaceA.show();
      _interfaceA.show();
      _logger.LogWarning("this is thirdController Index");
      base.ViewBag.Today = this._configuration["Today"];
      base.ViewBag.LogLevel = this._configuration["Logging:LogLevel:Default"];
      base.ViewBag.Say=this._configuration["Say"];
      return View();
    }
  }
}