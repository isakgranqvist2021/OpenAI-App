namespace OpenAIApp.Modules.History;

public interface HistoryInterface
{
    public Task<List<HistoryModel>?> Read();
    public Task Insert(HistoryModel searchResponseModel);
}