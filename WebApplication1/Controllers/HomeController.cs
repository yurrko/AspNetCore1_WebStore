using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {

        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }


        public IActionResult BlogSingle()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult NotFound()
        {
            return View();
        }
    }
}