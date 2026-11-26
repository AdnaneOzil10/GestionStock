using GestionStock.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionStock.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.NbCategories = await _context.Categories.CountAsync();
                ViewBag.NbProduits = await _context.Produits.CountAsync();
                ViewBag.NbClients = await _context.Clients.CountAsync();
            }
            catch (Exception ex)
            {
                ViewBag.NbCategories = ViewBag.NbProduits = ViewBag.NbClients = 0;
                TempData["Erreur"] = "Impossible de se connecter à la base : " + ex.Message;
            }
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
