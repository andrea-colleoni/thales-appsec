using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCoreLibrary.Context;
using NetCoreLibrary.Model;

namespace AspnetRazorPages.Pages.PersonaPages
{
    public class EditModel : PageModel
    {
        private readonly NetCoreLibrary.Context.PersoneContext _context;

        public EditModel(NetCoreLibrary.Context.PersoneContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Persona Persona { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Persona = await _context.Persone.FirstOrDefaultAsync(m => m.Email == id);

            if (Persona == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(Persona.Email))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PersonaExists(string id)
        {
            return _context.Persone.Any(e => e.Email == id);
        }
    }
}
