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

            var payload = new SearchPayloadModel
            {
                model = "text-davinci-003",
                prompt = searchString,
                temperature = .5f,
                max_tokens = 100,
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

            var SearchResponseModel = JsonSerializer.Deserialize<SearchResponseModel>(responseBody);

            if (SearchResponseModel is null)
            {
                throw new Exception("Could not deserialize search response");
            }

            await HistoryService.Insert(SearchResponseModel);
            return SearchResponseModel;
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

            var SearchResponseModels = await collection.FindAsync(new BsonDocument { });
            return SearchResponseModels.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public static async Task Insert(SearchResponseModel SearchResponseModel)
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

            await collection.InsertOneAsync(SearchResponseModel);
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
    public static List<SearchResponseModel>? sortSearchResponseModels(List<SearchResponseModel>? SearchResponseModels)
    {
        if (SearchResponseModels is null)
        {
            return null;
        }

        if (SearchResponseModels.Count() is 0)
        {
            return null;
        }

        SearchResponseModels.Sort(delegate (SearchResponseModel x, SearchResponseModel y)
        {
            if (y.created > x.created)
            {
                return 1;
            }

            return -1;
        });

        return SearchResponseModels;
    }
}