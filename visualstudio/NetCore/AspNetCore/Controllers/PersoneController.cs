using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCoreLibrary;
using NetCoreLibrary.Context;
using NetCoreLibrary.Model;
using NetCoreLibrary.Services;

namespace AspNetCore.Controllers
{
    public class PersoneController : Controller
    {
        private readonly PersoneContext _context;
        private readonly IPersonaService _service;

        public PersoneController(PersoneContext context, IPersonaService service)
        {
            _context = context;
            _service = service;
        }

        public IActionResult Test()
        {
            return View(_service.PersonaDiTest());
        }

        // GET: Persone
        public async Task<IActionResult> Index()
        {
            return View(await _context.Persone.ToListAsync());
        }

        // GET: Persone/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persone
                .FirstOrDefaultAsync(m => m.Email == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Persone/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Persone/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Nome,Cognome")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Persone/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persone.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Persone/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email,Nome,Cognome")] Persona persona)
        {
            if (id != persona.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Email))
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
            return View(persona);
        }

        // GET: Persone/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persone
                .FirstOrDefaultAsync(m => m.Email == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Persone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var persona = await _context.Persone.FindAsync(id);
            _context.Persone.Remove(persona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(string id)
        {
            return _context.Persone.Any(e => e.Email == id);
        }
    }
}
