using System;
using System.ComponentModel.DataAnnotations;

namespace FazendaUrbana.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data do Pedido")]
        public DateTime DataPedido { get; set; } = DateTime.Now;

        [Required]
        [StringLength(250)]
        public string Endereco { get; set; }

        public int? TransportadoraId { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        [StringLength(50)]
        public string MetodoEntrega { get; set; }

        [Required]
        public int Quantidade { get; set; } // Adicionando a propriedade 'Quantidade'

        [Required]
        public int ProdutoId { get; set; } // Adicionando a propriedade 'ProdutoId'

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}