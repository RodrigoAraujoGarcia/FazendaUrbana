// using Microsoft.AspNetCore.Mvc;
// using FazendaUrbana.Models;
// using Microsoft.EntityFrameworkCore;
// using System.Linq;
// using System.Threading.Tasks; // Adjust the namespace as necessary
// using Microsoft.Extensions.Logging;
// public class CarrinhoController : Controller
// {
//     private readonly FazendaUrbanaContext _context;

//     public CarrinhoController(FazendaUrbanaContext context)
//     {
//         _context = context;
//     }

//     public IActionResult Index()
//     {
//         var carrinho = HttpContext.Session.GetObjectFromJson<CarrinhoModel>("Carrinho") ?? new CarrinhoModel();
//         return View(carrinho);
//     }

//     public IActionResult AdicionarProduto(int id, int quantidade)
//     {
//         var produto = _context.Produtos.Find(id);
//         if (produto != null)
//         {
//             var carrinho = HttpContext.Session.GetObjectFromJson<CarrinhoModel>("Carrinho") ?? new CarrinhoModel();
//             carrinho.AdicionarItem(produto, quantidade);
//             HttpContext.Session.SetObjectAsJson("Carrinho", carrinho);
//         }

//         return RedirectToAction("Index");
//     }

//     public IActionResult RemoverProduto(int id)
//     {
//         var carrinho = HttpContext.Session.GetObjectFromJson<CarrinhoModel>("Carrinho") ?? new CarrinhoModel();
//         carrinho.RemoverItem(id);
//         HttpContext.Session.SetObjectAsJson("Carrinho", carrinho);

//         return RedirectToAction("Index");
//     }

//     public IActionResult FinalizarCompra()
//     {
//         var carrinho = HttpContext.Session.GetObjectFromJson<CarrinhoModel>("Carrinho");
//         if (carrinho == null || !carrinho.Itens.Any())
//         {
//             return RedirectToAction("Index");
//         }

//         // Aqui você pode implementar o código para salvar o pedido no banco de dados
//         // Exemplo:
//         var pedido = new PedidoModel
//         {
//             ValorTotal = carrinho.Total,
//             DataCompra = DateTime.Now,
//             Itens = carrinho.Itens.Select(i => new ItemPedidoModel
//             {
//                 IdProduto = i.Produto.Id,
//                 Quantidade = i.Quantidade,
//                 PrecoUnitario = i.Produto.Preco
//             }).ToList()
//         };

//         // Salvando o pedido no banco de dados
//         _context.Pedido.Add(pedido);
//         _context.SaveChanges();

//         // Limpar o carrinho
//         HttpContext.Session.Remove("Carrinho");

//         return View("CompraFinalizada", pedido);
//     }
// }