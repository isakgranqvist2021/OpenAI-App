using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OpenAIApp.Modules.User;

public class UserModel
{
    [BsonId]
    [BsonRequired]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId? Id { get; set; }

    [BsonRequired]
    [BsonElement("email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = "";

    [BsonRequired]
    [BsonElement("password")]
    public string Password { get; set; } = "";

    [BsonRequired]
    public DateTime Created { get; set; } = DateTime.Now;
}
