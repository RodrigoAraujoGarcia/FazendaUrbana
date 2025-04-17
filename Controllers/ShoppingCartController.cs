using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FazendaUrbana.Models; 


public class ShoppingCartController : Controller
{
    private readonly FazendaUrbanaContext _context;

    public ShoppingCartController(FazendaUrbanaContext context)
    {
        _context = context;
    }

    public IActionResult Index()
{
    // Retrieve the cart from the session
    ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart") ?? new ShoppingCart();

    // Get all products from the database
    var produtos = _context.Produtos.ToList();

    // Prepare the ViewModel
    var viewModel = new ShoppingCartViewModel
    {
        Cart = cart,
        Produtos = produtos
    };

    return View(viewModel); // Pass the view model to the view
}

 public IActionResult AddToCart(int id)
    {
        // Try to find the product by ID
        var product = _context.Produtos.Find(id);

        // Check if the product exists
        if (product != null)
        {
            // Retrieve the cart from the session or create a new one if it doesn't exist
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart") ?? new ShoppingCart();

            // Add the product to the cart - you can modify how many to add by changing the 1 below
            cart.AddToCart(product.Id, product.Nome, product.Preco, 1);
            
            // Save the cart back to session
            HttpContext.Session.SetObjectAsJson("ShoppingCart", cart);
        }
        else
        {
            // Optionally, you might want to handle the case where the product does not exist
            // For example, you could flash a message saying the product was not found.
            TempData["Error"] = "Produto não encontrado.";
        }

        // Redirecting to the cart view (or wherever you prefer)
        return RedirectToAction("Index");
    }
    public IActionResult RemoveFromCart(int id)
    {
        ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart") ?? new ShoppingCart();
        cart.RemoveFromCart(id);
        HttpContext.Session.SetObjectAsJson("ShoppingCart", cart);
        return RedirectToAction("Index");
    }

[HttpPost]
public IActionResult FinalizePurchase(string endereco, int? transportadoraId, string metodoEntrega)
{
    // Check if the user is logged in
    var userIdString = HttpContext.Session.GetString("UserId");
    if (string.IsNullOrEmpty(userIdString))
    {
        return RedirectToAction("Login", "Cliente"); // Redirect if user is not logged in
    }

    // Convert the UserId to an integer
    int clienteId = int.Parse(userIdString);

    // Retrieve the cart from the session
    var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart");

    if (cart != null && cart.Items.Any())
    {
        foreach (var item in cart.Items)
        {
            // Create a new order for each item in the cart
            var pedido = new Pedido
            {
                ProdutoId = item.ProdutoId,                  // Product ID from the cart
                Quantidade = item.Quantidade,                // Quantity from the cart
                ClienteId = clienteId,                       // Current logged-in user's ID
                Endereco = endereco,                         // Address provided during checkout
                TransportadoraId = transportadoraId,        // Carrier ID selected by the user
                MetodoEntrega = metodoEntrega,               // Delivery method selected by the user
                Status = "Pendente",                         // Set the initial order status
            };

            // Add the order to the context and save
            _context.Pedido.Add(pedido);
        }

        _context.SaveChanges();

        // Clear the cart after finalizing the purchase (optional)
        HttpContext.Session.Remove("ShoppingCart");

        return RedirectToAction("Index", "Home"); // Redirect to the homepage or other page
    }

    // If something went wrong, you may return an error message
    TempData["Error"] = "No items in the cart.";
    return RedirectToAction("Index");
}
}