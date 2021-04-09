using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OreLavorateLib.Context;
using OreLavorateLib.Model;

namespace OreLavorateCore.Controllers
{
    public class UtentiController : Controller
    {
        private readonly OrelavorateContext _context;

        public UtentiController(OrelavorateContext context)
        {
            _context = context;
        }

        // GET: Utenti
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utentes.ToListAsync());
        }

        // GET: Utenti/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utentes
                .FirstOrDefaultAsync(m => m.Username == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        // GET: Utenti/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utenti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utente);
        }

        // GET: Utenti/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utentes.FindAsync(id);
            if (utente == null)
            {
                return NotFound();
            }
            return View(utente);
        }

        // POST: Utenti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Password")] Utente utente)
        {
            if (id != utente.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtenteExists(utente.Username))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(utente);
        }

        // GET: Utenti/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utentes
                .FirstOrDefaultAsync(m => m.Username == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        // POST: Utenti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var utente = await _context.Utentes.FindAsync(id);
            _context.Utentes.Remove(utente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtenteExists(string id)
        {
            return _context.Utentes.Any(e => e.Username == id);
        }
    }
}
