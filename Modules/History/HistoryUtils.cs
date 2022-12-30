namespace OpenAIApp.Modules.History;

public class Sorter
{
    public static List<HistoryModel>? sortSearchResponseModels(List<HistoryModel>? searchResponseModels)
    {
        if (searchResponseModels is null)
        {
            return null;
        }

        if (searchResponseModels.Count() is 0)
        {
            return null;
        }

        searchResponseModels.Sort(delegate (HistoryModel x, HistoryModel y)
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