using FazendaUrbana.Models;
public class PedidoProduto
{
    public int Id { get; set; } // Primary key property

    public int PedidoId { get; set; } // Foreign key to Pedido
    public int ProdutoId { get; set; } // Foreign key to Produto
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }

    // Navigation properties
    public virtual Pedido Pedido { get; set; }
    public virtual ProdutoModel Produto { get; set; }
}