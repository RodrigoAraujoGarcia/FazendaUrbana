using System;
using System.ComponentModel.DataAnnotations;

namespace FazendaUrbana.Models
{
    public class Transportadora
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da transportadora é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da transportadora não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [StringLength(50, ErrorMessage = "O contato não pode exceder 50 caracteres.")]
        public string Contato { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser um número positivo.")]
        public int? Quantidade { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataEnvio { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataEntrega { get; set; }

        [StringLength(20, ErrorMessage = "O status não pode exceder 20 caracteres.")]
        public string Status { get; set; }
    }
}