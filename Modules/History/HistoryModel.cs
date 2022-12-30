using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace OpenAIApp.Modules.History;

public class ChoiceModel
{
    [BsonElement("text")]
    [BsonRequired]
    [JsonPropertyName("text")]
    public string Text { get; set; } = "";

    [BsonElement("index")]
    [BsonRequired]
    [JsonPropertyName("index")]
    public int Index { get; set; } = 0;

    [BsonElement("logprobs")]
    [BsonRequired]
    [JsonPropertyName("logprobs")]
    public string? LogProbs { get; set; } = null;

    [BsonElement("finish_reason")]
    [BsonRequired]
    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; set; } = "length";
}

public class UsageModel
{
    [BsonElement("prompt_tokens")]
    [BsonRequired]
    [JsonPropertyName("prompt_tokens")]
    public int PromptTokens { get; set; } = 0;

    [BsonElement("completion_tokens")]
    [BsonRequired]
    [JsonPropertyName("completion_tokens")]
    public int CompletionTokens { get; set; } = 0;

    [BsonElement("total_tokens")]
    [BsonRequired]
    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; set; } = 0;
}


public class HistoryModel
{
    [BsonElement("_id")]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonRequired]
    [JsonIgnore]
    public ObjectId? Id { get; set; }

    [BsonElement("searchString")]
    [BsonRequired]
    [JsonPropertyName("searchString")]
    public string SearchString { get; set; } = "";

    [BsonElement("obj")]
    [BsonRequired]
    [JsonPropertyName("obj")]
    public string Obj { get; set; } = "";

    [BsonElement("created")]
    [BsonRequired]
    [JsonPropertyName("created")]
    public long Created { get; set; } = 0;

    [BsonElement("model")]
    [BsonRequired]
    [JsonPropertyName("model")]
    public string Model { get; } = "text-davinci:003";

    [BsonElement("choices")]
    [BsonRequired]
    [JsonPropertyName("choices")]
    public List<ChoiceModel> Choices { get; set; } = new List<ChoiceModel>();

    [BsonElement("usage")]
    [BsonRequired]
    [JsonPropertyName("usages")]
    public UsageModel? Usage { get; set; } = null;
}
