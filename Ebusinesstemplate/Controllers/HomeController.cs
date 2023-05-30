using Ebusinesstemplate.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ebusinesstemplate.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

    }
}