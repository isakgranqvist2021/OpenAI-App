
namespace RecGen.Models;

public class SearchModel
{
    public string SearchString { get; set; } = "";
}

public class SearchPayload
{
    public string model { get; set; } = "";
    public string prompt { get; set; } = "";
    public float temperature { get; set; } = 0;
    public int max_tokens { get; set; } = 7;
}

public class SearchResponseChoice
{
    public string text { get; set; } = "";
    public int index { get; set; } = 0;
}

public class SearchResponse
{
    public string id { get; set; } = "";
    public int created { get; set; } = 0;
    public string model { get; } = "text-davinci:003";
    public List<SearchResponseChoice> choices { get; set; } = new List<SearchResponseChoice>();
}