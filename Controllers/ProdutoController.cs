using Microsoft.AspNetCore.Mvc;
using FazendaUrbana.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks; // Adjust the namespace as necessary
using Microsoft.Extensions.Logging;
using FazendaUrbana.Filters; 

public class ProdutoController : Controller
{
    private readonly ILogger<ProdutoController> _logger;
    private readonly FazendaUrbanaContext _context;
     public ProdutoController(FazendaUrbanaContext context, ILogger<ProdutoController> logger)
    {
        _context = context;
        _logger = logger; // Initialize the logger here
    }

    // public ProdutoController(FazendaUrbanaContext context)
    // {
    //     _context = context;
    // }

    public IActionResult Index()
    {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserCPF")))
            {
                return RedirectToAction("Login", "Funcionario");
            }
        var products = _context.Produtos.ToList();
        return View(products); // Pass products to the view 
    }
    public IActionResult Cindex()
    {
        var products = _context.Produtos.ToList();
        return View(products); // Pass products to the view 
    }
    public IActionResult Cdetails(int id)
    {
        var product = _context.Produtos.Find(id); // Fetch from DB
        if (product == null)
        {
            return NotFound();
        }
        
        return View(product);
    }

    public IActionResult Details(int id)
    {
        var product = _context.Produtos.Find(id); // Fetch from DB
        if (product == null)
        {
            return NotFound();
        }
        
        return View(product);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProdutoModel produto)
    {
        if (ModelState.IsValid) // Validate the model
        {
            _context.Produtos.Add(produto); // Add the new product to the context
            _context.SaveChanges(); // Save changes to the database
            return RedirectToAction(nameof(Index)); // Redirect to the Index action after saving
        }
        return View(produto);
        
         // If model state is invalid, return to the view with the current model
    }
        // GET: Produto/Edit/5
    public IActionResult Edit(int id)
    {
        var produto = _context.Produtos.Find(id);
        if (produto == null)
        {
            return NotFound();
        }
        return View(produto);
    }

    // POST: Produto/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProdutoModel produto)
    {
        if (id != produto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(produto); // This will update the existing product
                await _context.SaveChangesAsync(); // Save changes to the database
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(produto.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex) // Catch any other exceptions
        {
            // Log other exceptions as needed
            _logger.LogError($"An error occurred while updating the product: {ex.Message}");
        }
            return RedirectToAction(nameof(Index)); // Redirect to the Index action after saving
        }
        // Log the errors if ModelState is invalid
        var errors = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var error in errors)
        {
            _logger.LogError($"Validation error: {error.ErrorMessage}");
        }

        return View(produto); // If model state is invalid, return to the view with the current model
    }

    private bool ProdutoExists(int id)
    {
        return _context.Produtos.Any(e => e.Id == id);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto != null)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
