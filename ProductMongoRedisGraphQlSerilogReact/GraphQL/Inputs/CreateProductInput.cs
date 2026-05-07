namespace ProductMongoRedisGraphQlSerilogReact.GraphQL.Inputs;

//
public class CreateProductInput
{
    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public int Estoque { get; set; }
}