namespace Estoque.Worker.Models;

public class OrdemDeVenda
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string NomeCliente { get; set; } = string.Empty;
    public DateTime DataVenda { get; set; } = DateTime.UtcNow;
    public List<ItemVenda> Itens { get; set; } = new();
}