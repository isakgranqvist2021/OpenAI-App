using Microsoft.AspNetCore.Mvc;
using OpenAIApp.Config;
using OpenAIApp.Modules.Search;
using OpenAIApp.Modules.History;
using MongoDB.Bson;

namespace OpenAIApp.Controllers;

public class IndexTemplatePayload
{
    public List<HistoryModel>? SearchHistory { get; set; } = null;
    public HistoryModel? SearchResponse { get; set; } = null;
}

[Route("/")]
public class IndexController : Controller
{
    private SearchService _searchService;
    private HistoryService _historyService;

    public IndexController()
    {
        _searchService = new SearchService();
        _historyService = new HistoryService();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var id = HttpContext.Session.GetString("Session");

        if (id is null)
        {
            return Redirect("/sign-in");
        }

        var history = Sorter.sortSearchResponseModels(await _historyService.Read(ObjectId.Parse(id)));

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
            var id = HttpContext.Session.GetString("Session");

            if (id is null)
            {
                throw new Exception("User is null");
            }

            if (!ModelState.IsValid)
            {
                throw new Exception("Model state invalid");
            }

            var userId = ObjectId.Parse(id);
            var HistoryModel = await _searchService.Search(data.SearchString, userId);
            var history = Sorter.sortSearchResponseModels(await _historyService.Read(userId));

            return View(ViewPaths.IndexView, new IndexTemplatePayload
            {
                SearchHistory = history,
                SearchResponse = HistoryModel
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Redirect("/");
        }
    }
}
