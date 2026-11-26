using GestionStock.Data;
using GestionStock.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionStock.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _context.Categories
                    .Include(c => c.Produits)
                    .OrderBy(c => c.Nom)
                    .ToListAsync();
                return View(categories);
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur lors du chargement des catégories : " + ex.Message;
                return View(new List<Categorie>());
            }
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                var categorie = await _context.Categories
                    .Include(c => c.Produits)
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (categorie == null) return NotFound();
                return View(categorie);
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur lors de l'affichage du détail : " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom,Description")] Categorie categorie)
        {
            if (!ModelState.IsValid) return View(categorie);
            try
            {
                _context.Categories.Add(categorie);
                await _context.SaveChangesAsync();
                TempData["Succes"] = "Catégorie ajoutée avec succès.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Erreur base de données : " + (ex.InnerException?.Message ?? ex.Message));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erreur : " + ex.Message);
            }
            return View(categorie);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                var categorie = await _context.Categories.FindAsync(id);
                if (categorie == null) return NotFound();
                return View(categorie);
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur : " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description")] Categorie categorie)
        {
            if (id != categorie.Id) return NotFound();
            if (!ModelState.IsValid) return View(categorie);
            try
            {
                _context.Update(categorie);
                await _context.SaveChangesAsync();
                TempData["Succes"] = "Catégorie modifiée avec succès.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Categories.AnyAsync(c => c.Id == id)) return NotFound();
                ModelState.AddModelError("", "La catégorie a été modifiée par un autre utilisateur.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erreur : " + ex.Message);
            }
            return View(categorie);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                var categorie = await _context.Categories
                    .Include(c => c.Produits)
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (categorie == null) return NotFound();
                return View(categorie);
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur : " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var categorie = await _context.Categories.FindAsync(id);
                if (categorie != null)
                {
                    _context.Categories.Remove(categorie);
                    await _context.SaveChangesAsync();
                }
                TempData["Succes"] = "Catégorie supprimée avec succès.";
            }
            catch (DbUpdateException)
            {
                TempData["Erreur"] = "Impossible de supprimer cette catégorie : elle contient des produits.";
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur lors de la suppression : " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
