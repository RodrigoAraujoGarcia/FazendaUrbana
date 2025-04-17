using System.ComponentModel.DataAnnotations.Schema;
namespace FazendaUrbana.Models
{
    public class FuncionarioModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; } // Consider using string for CPF

        public string Telefone { get; set; } // Consider using string for Telefone

        public string Cargo { get; set; }
        
        public DateTime DataDeAdmissao { get; set; } // Change to DateTime
        
        public string CargaHoraria { get; set; }
        public string Senha {get;set;}
    }
}