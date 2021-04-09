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
    public class IndexModel : PageModel
    {
        private readonly NetCoreLibrary.Context.PersoneContext _context;

        public IndexModel(NetCoreLibrary.Context.PersoneContext context)
        {
            _context = context;
        }

        public IList<Persona> Persona { get;set; }

        public async Task OnGetAsync()
        {
            Persona = await _context.Persone.ToListAsync();
        }
    }
}
