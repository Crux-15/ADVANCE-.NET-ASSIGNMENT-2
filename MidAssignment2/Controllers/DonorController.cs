using Microsoft.AspNetCore.Mvc;
using MidAssignment2.EF;

namespace MidAssignment2.Controllers
{
    public class DonorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
