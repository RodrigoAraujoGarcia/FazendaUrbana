using Microsoft.EntityFrameworkCore;

namespace FazendaUrbana.Models
{
    public class FazendaUrbanaContext : DbContext
    {
        public FazendaUrbanaContext(DbContextOptions<FazendaUrbanaContext> options) : base(options) { }

        public DbSet<ProdutoModel> Produtos { get; set; } 
        public DbSet<ClienteModel> Clientes { get; set; }// Map to the Produto table
        public DbSet<FuncionarioModel> Funcionarios {get; set;}
        public DbSet<Pedido> Pedido { get; set; } // Corrected naming from `Pedido` to `Pedidos`
        public DbSet<Transportadora> Transportadora { get; set; } // Ensure this is here
        public DbSet<PedidoProduto> PedidoProduto { get; set; }
    }
}