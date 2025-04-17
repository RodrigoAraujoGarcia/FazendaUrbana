using System.Collections.Generic;
using System.Linq;
namespace FazendaUrbana.Models
{
    public class CartItem
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }

    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddToCart(int produtoId, string nome, decimal preco, int quantidade)
        {
            var existingItem = Items.FirstOrDefault(x => x.ProdutoId == produtoId);
            if (existingItem != null)
            {
                existingItem.Quantidade += quantidade;
            }
            else
            {
                Items.Add(new CartItem { ProdutoId = produtoId, Nome = nome, Preco = preco, Quantidade = quantidade });
            }
        }

        public void RemoveFromCart(int produtoId)
        {
            Items.RemoveAll(x => x.ProdutoId == produtoId);
        }

        public decimal Total => Items.Sum(x => x.Preco * x.Quantidade);

        public void Clear()
        {
            Items.Clear();
        }
    }
}