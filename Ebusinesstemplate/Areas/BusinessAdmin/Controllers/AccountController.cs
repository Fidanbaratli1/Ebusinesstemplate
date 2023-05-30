using Microsoft.AspNetCore.Mvc;

namespace Ebusinesstemplate.Areas.BusinessAdmin.Controllers
{

    [Area("BusinessAdmin")]
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
