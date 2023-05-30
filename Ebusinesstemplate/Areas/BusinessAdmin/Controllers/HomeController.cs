using Microsoft.AspNetCore.Mvc;

namespace Ebusinesstemplate.Areas.BusinessAdmin.Controllers
{
    [Area("BusinessAdmin")]
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
