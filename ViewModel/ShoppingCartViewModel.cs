namespace FazendaUrbana.Models{
public class ShoppingCartViewModel
{
    public ShoppingCart Cart { get; set; }
    public IEnumerable<ProdutoModel> Produtos { get; set; }
}
}