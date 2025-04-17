// using Microsoft.AspNetCore.Mvc;
// using FazendaUrbana.Models;
// using Microsoft.EntityFrameworkCore;
// using System.Linq;
// using System.Threading.Tasks; // Adjust the namespace as necessary
// using Microsoft.Extensions.Logging;
// using FazendaUrbana.Filters; 


// namespace FazendaUrbana.Controllers;

// public class PedidosController : Controller
// {
//     private readonly FazendaUrbanaContext _context;
//     private readonly ILogger<PedidosController> _logger;

//     // Constructor should be named PetidosController
//     public PedidosController(FazendaUrbanaContext context, ILogger<PedidosController> logger)
//     {
//         _context = context;
//         _logger = logger; // Initialize the logger here
//     }

//     // GET: Pedidos
//     public IActionResult Index()
//     {
//         if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
//         {
//             return RedirectToAction("Login", "Cliente");
//         }
//         var pedidos = _context.Pedido.ToList();
//         return View(pedidos);
//     }

//     // GET: Pedidos/Create
//     public IActionResult Create()
//     {
//         ViewBag.Clientes = _context.Clientes.ToList();
//         ViewBag.Transportadora = _context.Transportadora.ToList();
//         return View();
//     }

//     // POST: Pedidos/Create
//     [HttpPost]
//     [ValidateAntiForgeryToken]
//     public IActionResult Create(Pedido pedido)
//     {
//         if (ModelState.IsValid)
//         {
//             _context.Add(pedido);
//             _context.SaveChanges();
//             return RedirectToAction(nameof(Index));
//         }
//         return View(pedido);
//     }

//     // GET: Pedidos/Edit/5
//     public IActionResult Edit(int id)
//     {
//         var pedido = _context.Pedido.Find(id);
//         if (pedido == null)
//         {
//             return NotFound();
//         }

//         ViewBag.Clientes = _context.Clientes.ToList();
//         ViewBag.Transportadora = _context.Transportadora.ToList();
//         return View(pedido);
//     }

//     // POST: Pedidos/Edit/5
//     [HttpPost]
//     [ValidateAntiForgeryToken]
//     public IActionResult Edit(int id, Pedido pedido)
//     {
//         if (id != pedido.Id)
//         {
//             return NotFound();
//         }

//         if (ModelState.IsValid)
//         {
//             try
//             {
//                 _context.Update(pedido);
//                 _context.SaveChanges();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!_context.Pedido.Any(e => e.Id == pedido.Id))
//                 {
//                     return NotFound();
//                 }
//                 else
//                 {
//                     throw; // Rethrow exception if there's a concurrency problem
//                 }
//             }
//             return RedirectToAction(nameof(Index));
//         }

//         // Re-populate ViewBag if the ModelState is invalid
//         ViewBag.Clientes = _context.Clientes.ToList();
//         ViewBag.Transportadora = _context.Transportadora.ToList();
//         return View(pedido);
//     }

//     // GET: Pedidos/Delete/5
// public IActionResult Delete(int id)
// {
//     var pedido = _context.Pedido.Find(id);
//     if (pedido == null)
//     {
//         return NotFound();
//     }
//     return View(pedido);
// }

// // POST: Pedidos/Delete
// [HttpPost, ActionName("Delete")]
// [ValidateAntiForgeryToken]
// public IActionResult DeleteConfirmed(int id)
// {
//     _logger.LogInformation("Attempting to delete Pedido with ID: {Id}", id);
//     var pedido = _context.Pedido.Find(id);

//     if (pedido == null)
//     {
//         _logger.LogWarning("Pedido with ID: {Id} not found.", id);
//         return NotFound();
//     }

//     _context.Pedido.Remove(pedido); // Remove the ordered item
//     _context.SaveChanges(); // Commit transaction
//     _logger.LogInformation("Pedido with ID: {Id} deleted successfully.", id);
//     return RedirectToAction(nameof(Index));
// }
// }