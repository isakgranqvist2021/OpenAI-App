namespace OpenAIApp.Modules.Search;

public class SearchModel
{
    public string SearchString { get; set; } = "";
}

public class SearchPayloadModel
{
    public string model { get; set; } = "";
    public string prompt { get; set; } = "";
    public float temperature { get; set; } = 0;
    public int max_tokens { get; set; } = 7;
}

public class SearchResponseChoiceModel
{
    public string text { get; set; } = "";
    public int index { get; set; } = 0;
    public string? logprobs { get; set; } = null;
    public string finish_reason { get; set; } = "length";
}

public class UsageModel
{
    public int prompt_tokens { get; set; } = 0;
    public int completion_tokens { get; set; } = 0;
    public int total_tokens { get; set; } = 0;
}


public class SearchResponseModel
{
    public string id { get; set; } = "";
    public string obj { get; set; } = "";
    public long created { get; set; } = 0;
    public string model { get; } = "text-davinci:003";
    public List<SearchResponseChoiceModel> choices { get; set; } = new List<SearchResponseChoiceModel>();
    public UsageModel? usage { get; set; } = null;
}
