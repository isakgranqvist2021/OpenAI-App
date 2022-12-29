using Microsoft.AspNetCore.Mvc;
using OpenAIApp.Config;
using OpenAIApp.Modules.Search;

namespace OpenAIApp.Controllers;

public class IndexTemplatePayload
{
    public List<SearchResponseModel>? searchHistory { get; set; } = null;
    public SearchResponseModel? searchResponse { get; set; } = null;
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
            searchHistory = history,
            searchResponse = null
        });
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromForm] SearchModel data)
    {
        var SearchResponseModel = await _searchService.Search(data.SearchString);
        var history = Sorter.sortSearchResponseModels(await HistoryService.Read());

        return View(ViewPaths.IndexView, new IndexTemplatePayload
        {
            searchHistory = history,
            searchResponse = SearchResponseModel
        });
    }
}
