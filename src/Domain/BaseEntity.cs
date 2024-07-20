using Domain.Abstraction;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json;

namespace Domain;

public abstract class BaseEntity : IAggregationRoot
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
