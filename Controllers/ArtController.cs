using Microsoft.AspNetCore.Mvc;

namespace Activity5_CRUD.Controllers
{
    public class ArtController : Controller
    {
        public IActionResult Entries()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
