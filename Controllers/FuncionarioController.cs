using Microsoft.AspNetCore.Mvc;
using FazendaUrbana.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks; // Adjust the namespace as necessary
using Microsoft.Extensions.Logging;
using FazendaUrbana.Filters; 
using Microsoft.AspNetCore.Identity;

namespace FazendaUrbana.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly FazendaUrbanaContext _context;

        public FuncionarioController(FazendaUrbanaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserCPF")))
            {
                return RedirectToAction("Login");
            }
            var funcionarios = await _context.Funcionarios.ToListAsync();
            return View(funcionarios);
        }

        // Other actions (Create, Edit, Delete) will go here

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Signup
        [HttpPost]
        public async Task<IActionResult> Create(FuncionarioModel funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Funcionarios.Add(funcionario);
                await _context.SaveChangesAsync(); // Await the SaveChangesAsync
                return RedirectToAction("Login");
            }
            return View(funcionario);
        }

                [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Funcionario/Login
        [HttpPost]
        public IActionResult Login(string CPF, string senha)
        {
            var funcionario = _context.Funcionarios.FirstOrDefault(c => c.CPF == CPF && c.Senha == senha);
            if (funcionario != null)
            {
                // Você pode armazenar informações do usuário na sessão
                HttpContext.Session.SetString("UserName", funcionario.Nome);
                HttpContext.Session.SetString("UserCPF", funcionario.CPF);
                HttpContext.Session.SetString("UserCargo", funcionario.Cargo);

                return RedirectToAction("Fpage", "Funcionario"); // Redirecionar para a página do funcionário
            }
            ModelState.AddModelError("", "CPF ou senha incorretos");
            return View();
        }
        
        [HttpPost]
        public IActionResult Logout()
        {
            // Clear session data
            HttpContext.Session.Clear();

            // Log the logout event if needed
            // _logger.LogInformation("User logged out.");

            // Redirect to the Login page
            return RedirectToAction("Login", "Funcionario");
        }

        public IActionResult Fpage()
        {
            // Verifica se o usuário está autenticado
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserCPF")))
            {
                return RedirectToAction("Login");
            }

            // Pega dados da sessão e armazena em ViewBag
            ViewBag.UserCPF = HttpContext.Session.GetString("UserCPF");
            ViewBag.UserCargo = HttpContext.Session.GetString("UserCargo");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            return View(); // Retorna a view que mostra a página do funcionário
        }
    }
}