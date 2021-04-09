using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetCoreLibrary.Context;
using NetCoreLibrary.Model;

namespace AspnetRazorPages.Pages.PersonaPages
{
    public class DetailsModel : PageModel
    {
        private readonly NetCoreLibrary.Context.PersoneContext _context;

        public DetailsModel(NetCoreLibrary.Context.PersoneContext context)
        {
            _context = context;
        }

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
    }
}
