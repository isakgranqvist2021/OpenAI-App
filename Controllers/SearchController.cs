using Microsoft.AspNetCore.Mvc;
using RecGen.Config;
using RecGen.Models;
using RecGen.Services;

namespace RecGen.Controllers;

[Route("/search")]
public class SearchController : Controller
{
    private readonly SearchService _searchService = new SearchService();

    [HttpPost]
    public async Task<ActionResult> Post([FromForm] SearchModel data)
    {
        var res = await _searchService.Search(data.SearchString);

        if (res is null)
        {
            return Redirect("/");
        }

        return View(ViewPaths.IndexView, res);
    }
}
