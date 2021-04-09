using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetCoreLibrary.Model;

namespace AspnetRazorPages.Pages.PersonaPages
{
    public class DeleteModel : PageModel
    {
        private readonly NetCoreLibrary.Context.PersoneContext _context;

        public DeleteModel(NetCoreLibrary.Context.PersoneContext context)
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Persona = await _context.Persone.FindAsync(id);

            if (Persona != null)
            {
                _context.Persone.Remove(Persona);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
