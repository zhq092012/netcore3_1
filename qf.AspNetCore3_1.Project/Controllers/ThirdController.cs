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
        private readonly InterfaceB _interfaceB;
        private readonly InterfaceC _interfaceC;

        public ThirdController(ILogger<ThirdController> logger, IConfiguration configuration, InterfaceA interfaceA, InterfaceB interfaceB, InterfaceC interfaceC)
        {
            _logger = logger;
            _configuration = configuration;
            _interfaceA = interfaceA;
            _interfaceB = interfaceB;
            _interfaceC = interfaceC;
        }
        public IActionResult Index()
        {
            // InterfaceA interfaceA=new ServiceA();
            // interfaceA.show();

            _logger.LogWarning("this is thirdController Index");
            base.ViewBag.Today = this._configuration["Today"];
            base.ViewBag.LogLevel = this._configuration["Logging:LogLevel:Default"];
            base.ViewBag.Say = this._configuration["Say"];
            base.ViewBag.A = _interfaceA.show();
            base.ViewBag.B = _interfaceB.show();
            base.ViewBag.C = _interfaceC.show();

            return View();
        }
    }
}