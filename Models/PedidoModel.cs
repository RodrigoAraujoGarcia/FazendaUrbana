public class PedidoModel
{
    public int Id { get; set; }
    public int IdCliente { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime DataCompra { get; set; }
    public List<ItemPedidoModel> Itens { get; set; } = new List<ItemPedidoModel>();
}

public class ItemPedidoModel
{
    public int Id { get; set; }
    public int IdProduto { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}