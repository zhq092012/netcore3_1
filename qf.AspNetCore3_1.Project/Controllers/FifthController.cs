using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using qf.AspNetCore3_1.Project.Utility;

namespace qf.AspNetCore3_1.Project.Controllers
{
    public class FifthController : Controller
    {
        [CustomExceptionFilter]
        public IActionResult Index()
        {
            throw new Exception("new excetion");
            return View();
        }
        [CustomExceptionFilter]
        public IActionResult Info()
        {
            throw new Exception("new excetion Info");
            return View();
        }

    }
}