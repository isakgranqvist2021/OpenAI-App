using MongoDB.Bson;
using MongoDB.Driver;
using OpenAIApp.Modules.Database;
using System.Text;
using System.Text.Json;

namespace OpenAIApp.Modules.Search;

public class SearchService
{
    private static HttpClient? _client;
    private const string baseUrl = "https://api.openai.com/v1/completions";
    public SearchService()
    {
        _client = new HttpClient();

        var openApiSecret = System.Environment.GetEnvironmentVariable("OPEN_API_SECRET");
        var bearerTokenHeader = String.Format("Bearer {0}", openApiSecret);
        _client.DefaultRequestHeaders.Add("Authorization", bearerTokenHeader);
    }

    public async Task<SearchResponseModel?> Search(string searchString)
    {
        try
        {
            if (_client is null)
            {
                throw new Exception("Http Client is null");
            }

            var payload = new SearchPayload
            {
                Model = "text-davinci-003",
                Prompt = searchString,
                Temperature = .5f,
                MaxTokens = 100,
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync(baseUrl, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (responseBody is null)
            {
                throw new Exception("Response body is null");
            }

            var searchResponse = JsonSerializer.Deserialize<SearchResponseModel>(responseBody);

            if (searchResponse is null)
            {
                throw new Exception("Could not deserialize search response");
            }

            searchResponse.SearchString = searchString;

            await HistoryService.Insert(searchResponse);

            return searchResponse;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}

public class HistoryService
{
    public static async Task<List<SearchResponseModel>?> Read()
    {
        try
        {
            var collection = Collections.GetCollection<SearchResponseModel>(
                Config.Collections.HistoryCollectionName
            );

            if (collection is null)
            {
                throw new Exception("Collection is null");
            }

            var history = await collection.FindAsync(new BsonDocument { });

            return history.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public static async Task Insert(SearchResponseModel searchResponseModel)
    {
        try
        {
            var collection = Collections.GetCollection<BsonDocument>(
                Config.Collections.HistoryCollectionName
            );

            if (collection is null)
            {
                throw new Exception("Collection is null");
            }

            var bson = searchResponseModel.ToBsonDocument();
            await collection.InsertOneAsync(bson);
            return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
    }
}

public class Sorter
{
    public static List<SearchResponseModel>? sortSearchResponseModels(List<SearchResponseModel>? searchResponseModels)
    {
        if (searchResponseModels is null)
        {
            return null;
        }

        if (searchResponseModels.Count() is 0)
        {
            return null;
        }

        searchResponseModels.Sort(delegate (SearchResponseModel x, SearchResponseModel y)
        {
            if (y.Created > x.Created)
            {
                return 1;
            }

            return -1;
        });

        return searchResponseModels;
    }
}