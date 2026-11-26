using GestionStock.Data;
using GestionStock.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionStock.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Clients  (liste + recherche avancée sur tous les attributs)
        public async Task<IActionResult> Index(ClientRecherche recherche)
        {
            try
            {
                // La requête est construite dynamiquement puis exécutée par SQL Server
                IQueryable<Client> query = _context.Clients.AsQueryable();

                if (recherche.Id.HasValue)
                    query = query.Where(c => c.Id == recherche.Id.Value);

                if (!string.IsNullOrWhiteSpace(recherche.Nom))
                    query = query.Where(c => c.Nom.Contains(recherche.Nom.Trim()));

                if (!string.IsNullOrWhiteSpace(recherche.Prenom))
                    query = query.Where(c => c.Prenom.Contains(recherche.Prenom.Trim()));

                if (!string.IsNullOrWhiteSpace(recherche.Email))
                    query = query.Where(c => c.Email.Contains(recherche.Email.Trim()));

                if (!string.IsNullOrWhiteSpace(recherche.Telephone))
                    query = query.Where(c => c.Telephone != null && c.Telephone.Contains(recherche.Telephone.Trim()));

                if (!string.IsNullOrWhiteSpace(recherche.Ville))
                    query = query.Where(c => c.Ville != null && c.Ville.Contains(recherche.Ville.Trim()));

                if (recherche.AgeMin.HasValue)
                    query = query.Where(c => c.Age >= recherche.AgeMin.Value);

                if (recherche.AgeMax.HasValue)
                    query = query.Where(c => c.Age <= recherche.AgeMax.Value);

                if (recherche.DateDebut.HasValue)
                    query = query.Where(c => c.DateInscription >= recherche.DateDebut.Value.Date);

                if (recherche.DateFin.HasValue)
                    query = query.Where(c => c.DateInscription <= recherche.DateFin.Value.Date);

                recherche.Resultats = await query.OrderBy(c => c.Nom).ThenBy(c => c.Prenom).ToListAsync();
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur lors de la recherche : " + ex.Message;
                recherche.Resultats = new List<Client>();
            }
            return View(recherche);
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
                if (client == null) return NotFound();
                return View(client);
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur lors de l'affichage du détail : " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View(new Client());
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom,Prenom,Email,Telephone,Ville,Age,DateInscription")] Client client)
        {
            if (!ModelState.IsValid) return View(client);
            try
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                TempData["Succes"] = "Client ajouté avec succès.";
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
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                var client = await _context.Clients.FindAsync(id);
                if (client == null) return NotFound();
                return View(client);
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur : " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Email,Telephone,Ville,Age,DateInscription")] Client client)
        {
            if (id != client.Id) return NotFound();
            if (!ModelState.IsValid) return View(client);
            try
            {
                _context.Update(client);
                await _context.SaveChangesAsync();
                TempData["Succes"] = "Client modifié avec succès.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Clients.AnyAsync(c => c.Id == id)) return NotFound();
                ModelState.AddModelError("", "Le client a été modifié par un autre utilisateur.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erreur : " + ex.Message);
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
                if (client == null) return NotFound();
                return View(client);
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur : " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var client = await _context.Clients.FindAsync(id);
                if (client != null)
                {
                    _context.Clients.Remove(client);
                    await _context.SaveChangesAsync();
                }
                TempData["Succes"] = "Client supprimé avec succès.";
            }
            catch (Exception ex)
            {
                TempData["Erreur"] = "Erreur lors de la suppression : " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
