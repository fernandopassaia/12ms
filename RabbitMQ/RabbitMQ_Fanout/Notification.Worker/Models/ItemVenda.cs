namespace Notification.Worker.Models;

public class ItemVenda
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string DescricaoProduto { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public int Quantidade { get; set; }
}