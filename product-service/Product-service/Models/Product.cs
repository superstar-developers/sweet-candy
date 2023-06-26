using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Product_service.Models;

// public class Product
// {
//     public long Id { get; set; }
//     public string? Name { get; set; }
//     public bool IsComplete { get; set; }
//     public string? Secret { get; set; }
// }

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] 
    public string? Id { get; set; }

    [BsonElement("Name")]
    [JsonPropertyName("Name")]
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
    public string? Secret { get; set; }
}