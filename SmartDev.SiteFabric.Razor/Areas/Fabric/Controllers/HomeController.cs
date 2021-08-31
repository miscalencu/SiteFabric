using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDev.SiteFabric.Razor.Areas.Fabric.Controllers
{
    [Area("Fabric")]
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            // return Ok("Welcome to Fabric admin!");
            return View("Index2");
        }
    }
}
