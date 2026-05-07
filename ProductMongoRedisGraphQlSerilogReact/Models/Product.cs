using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProductMongoRedisGraphQlSerilogReact.Interfaces;

namespace ProductMongoRedisGraphQlSerilogReact.Models;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public int Estoque { get; set; }

    public DateTime DataCadastro { get; set; }
}