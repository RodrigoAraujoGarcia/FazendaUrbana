using Microsoft.AspNetCore.Mvc;
using FazendaUrbana.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks; // Adjust the namespace as necessary
using Microsoft.Extensions.Logging;

public class ClienteController : Controller
{
    private readonly FazendaUrbanaContext _context;

    public ClienteController(FazendaUrbanaContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var clientes = _context.Clientes.ToList(); // Fetching all clients
        return View(clientes);
    }
    

    // GET: Cliente/Signup
    [HttpGet]
    public IActionResult Signup()
    {
        return View();
    }

    // POST: Cliente/Signup
    [HttpPost]
    public IActionResult Signup(ClienteModel cliente)
    {
        if (ModelState.IsValid)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }
        return View(cliente);
    }

    // GET: Cliente/Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: Cliente/Login
    [HttpPost]
    public IActionResult Login(string email, string senha)
    {
        var cliente = _context.Clientes.FirstOrDefault(c => c.Email == email && c.Senha == senha);
        if (cliente != null)
        {
            // Set session variables upon successful login
            HttpContext.Session.SetString("UserId", cliente.Id.ToString());
            HttpContext.Session.SetString("UserName", cliente.Nome);
            HttpContext.Session.SetString("UserEmail", cliente.Email);
            
            return RedirectToAction("Index", "Home"); // Redirect to a home page or dashboard
        }

        ModelState.AddModelError("", "Email or password incorrect");
        return View();
    }

    [HttpPost]
    public IActionResult Logout()
    {
        // Clear the session upon logout
        HttpContext.Session.Clear();
        
        return RedirectToAction("Login", "Cliente"); // Redirect to Login or Home page
    }
    public IActionResult OrderStatus()
{
    var userIdString = HttpContext.Session.GetString("UserId");
    if (string.IsNullOrEmpty(userIdString))
    {
        return RedirectToAction("Login", "Cliente"); // Redirect if not logged in
    }

    int clienteId = int.Parse(userIdString);

    // Fetching order statuses for the logged-in user
    var pedidos = _context.Pedido
                          .Where(p => p.ClienteId == clienteId)
                          .Select(p => new OrderStatusViewModel
                          {
                              PedidoId = p.Id,
                              Status = p.Status,
                              ProdutoNome = _context.Produtos
                                .Where(prod => prod.Id == p.ProdutoId)
                                .Select(prod => prod.Nome)
                                .FirstOrDefault()
                          })
                          .ToList();

    return View(pedidos);
}
}

