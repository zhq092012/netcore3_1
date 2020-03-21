using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using qf.AspNetCore3_1.Project.Models;

namespace qf.AspNetCore3_1.Project.Controllers
{

  public class FirstController : Controller
  {
    private readonly ILogger<FirstController> _logger;

    public FirstController(ILogger<FirstController> logger)
    {
      _logger = logger;
    }
    public IActionResult Index()
    {
      this._logger.LogWarning("this is log of first controller");
      #region ViewData
      base.ViewData["User"] = new CurrentUser()
      {
        Id = 7,
        Name = "Y",
        Account = "花心",
        Email = "zhq_092012@163.com",
        Password = "123",
        LoginTime = DateTime.Now
      };
      base.ViewData["something"] = 123456;
      #endregion
      #region ViewBag
      base.ViewBag.User = new CurrentUser()
      {
        Id = 6,
        Name = "Yyyy",
        Account = "花心111",
        Email = "zhq_092012@163.com",
        Password = "123",
        LoginTime = DateTime.Now
      };
      base.ViewData["something"] = 123456;
      #endregion
      #region TempData
      base.TempData["User"] = new CurrentUser()
      {
        Id = 5,
        Name = "Yyyy123",
        Account = "花心6661",
        Email = "zhq_092012@163.com",
        Password = "123",
        LoginTime = DateTime.Now
      };
      base.ViewData["something"] = 123456;
      #endregion
      if (string.IsNullOrWhiteSpace(this.HttpContext.Session.GetString("UserSession")))
      {
        base.HttpContext.Session.SetString("UserSession", Newtonsoft.Json.JsonConvert.SerializeObject(new CurrentUser()
        {
          Id = 5,
          Name = "Yyyy123",
          Account = "花心6661",
          Email = "zhq_092012@163.com",
          Password = "123",
          LoginTime = DateTime.Now
        }));
      }
      return View(new CurrentUser()
      {
        Id = 4,
        Name = "admin",
        Account = "system",
        Email = "zhq_092012@163.com",
        Password = "1",
        LoginTime = DateTime.Now
      });
    }
  }
}