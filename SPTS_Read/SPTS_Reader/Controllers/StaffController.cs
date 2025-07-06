using Microsoft.AspNetCore.Mvc;

namespace SPTS_Reader.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
