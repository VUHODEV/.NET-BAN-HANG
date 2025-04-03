using Microsoft.AspNetCore.Mvc;

namespace Lab03.Controllers
{
    public class OrderHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
