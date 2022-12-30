using Microsoft.AspNetCore.Mvc;
using OpenAIApp.Config;
using OpenAIApp.Modules.Search;

namespace OpenAIApp.Controllers;

public class IndexTemplatePayload
{
    public List<SearchResponseModel>? SearchHistory { get; set; } = null;
    public SearchResponseModel? SearchResponse { get; set; } = null;
}

[Route("/")]
public class IndexController : Controller
{
    private readonly SearchService _searchService = new SearchService();

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var history = Sorter.sortSearchResponseModels(await HistoryService.Read());

        return View(ViewPaths.IndexView, new IndexTemplatePayload
        {
            SearchHistory = history,
            SearchResponse = null
        });
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromForm] SearchBody data)
    {
        try
        {
            var SearchResponseModel = await _searchService.Search(data.SearchString);
            var history = Sorter.sortSearchResponseModels(await HistoryService.Read());

            return View(ViewPaths.IndexView, new IndexTemplatePayload
            {
                SearchHistory = history,
                SearchResponse = SearchResponseModel
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Redirect("/");
        }
    }
}
