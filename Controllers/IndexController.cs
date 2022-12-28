using Microsoft.AspNetCore.Mvc;
using RecGen.Config;

namespace RecGen.Controllers;

[Route("/")]
public class IndexController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(ViewPaths.IndexView);
    }
}
