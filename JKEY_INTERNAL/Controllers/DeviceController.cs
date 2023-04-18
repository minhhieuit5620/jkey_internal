using Microsoft.AspNetCore.Mvc;

namespace JKEY_INTERNAL.Controllers
{
    public class DeviceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
