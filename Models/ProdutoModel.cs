namespace FazendaUrbana.Models
{
    public class ProdutoModel
    {
    public int Id{get;set;}
    //[Required(ErrorMessage = "O campo nome é obrigatório.")]
    public string Nome {get;set;}

    //[Required(ErrorMessage = "O campo Preco é obrigatório.")]
    public decimal Preco {get;set;}

    public string Categoria{get;set;}

    public string Descricao{get;set;}

        
    }
}