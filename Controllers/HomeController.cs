using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FazendaUrbana.Models;

namespace FazendaUrbana.Controllers;

public class HomeController : Controller
{
    private readonly FazendaUrbanaContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(FazendaUrbanaContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }
    public IActionResult AboutUs(){
        return View();
    }
    public IActionResult Index()
    {
        // Get the first two products from the database
        var products = _context.Produtos.Take(2).ToList();
        return View(products);
    }
    public IActionResult TalkToUs(){
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
