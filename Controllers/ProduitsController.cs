using GestionStock.Data;
using GestionStock.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionStock.Controllers
{
    public class ProduitsController : Controller
    {
        private readonly AppDbContext _context;

        public ProduitsController(AppDbContext context)
        {
            _context = context;
        }

        // Remplit la liste déroulante des catégories
        private async Task ChargerCategories(int? selection = null)
        {
            var categories = await _context.Categories.OrderBy(c => c.Nom).ToListAsync();
            ViewBag.CategorieId = new SelectList(categories, "Id", "Nom", selection);
        }

        // GET: Produits
        public async Task<IActionResult> Index()
        {
            try
            {
                var produits = await _context.Produits
                    .Include(p => p.Categorie)
                    .OrderBy(p => p.Nom)
                    .ToListAsync();
                return View(produits);
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur lors du chargement des produits : " + ex.Message;
                return View(new List<Produit>());
            }
        }

        // GET: Produits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                var produit = await _context.Produits
                    .Include(p => p.Categorie)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (produit == null) return NotFound();
                return View(produit);
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur lors de l'affichage du détail : " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Produits/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                await ChargerCategories();
                return View(new Produit());
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur : " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Produits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom,Prix,Quantite,DateAjout,CategorieId")] Produit produit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Produits.Add(produit);
                    await _context.SaveChangesAsync();
                    TempData["Succes"] = "Produit ajouté avec succès.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Erreur base de données : " + (ex.InnerException?.Message ?? ex.Message));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erreur : " + ex.Message);
            }
            await ChargerCategories(produit.CategorieId);
            return View(produit);
        }

        // GET: Produits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                var produit = await _context.Produits.FindAsync(id);
                if (produit == null) return NotFound();
                await ChargerCategories(produit.CategorieId);
                return View(produit);
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur : " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Produits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prix,Quantite,DateAjout,CategorieId")] Produit produit)
        {
            if (id != produit.Id) return NotFound();
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(produit);
                    await _context.SaveChangesAsync();
                    TempData["Succes"] = "Produit modifié avec succès.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Produits.AnyAsync(p => p.Id == id)) return NotFound();
                ModelState.AddModelError("", "Le produit a été modifié par un autre utilisateur.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erreur : " + ex.Message);
            }
            await ChargerCategories(produit.CategorieId);
            return View(produit);
        }

        // GET: Produits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                var produit = await _context.Produits
                    .Include(p => p.Categorie)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (produit == null) return NotFound();
                return View(produit);
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur : " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var produit = await _context.Produits.FindAsync(id);
                if (produit != null)
                {
                    _context.Produits.Remove(produit);
                    await _context.SaveChangesAsync();
                }
                TempData["Succes"] = "Produit supprimé avec succès.";
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur lors de la suppression : " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
