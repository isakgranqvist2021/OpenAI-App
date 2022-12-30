using MongoDB.Bson;

namespace OpenAIApp.Modules.Session;


[Serializable]
public class SessionModel
{
    public ObjectId? Id { get; set; }
}