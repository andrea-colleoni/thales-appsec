using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetCoreLibrary.Model;

namespace AspnetRazorPages.Pages.PersonaPages
{
    public class CreateModel : PageModel
    {
        private readonly NetCoreLibrary.Context.PersoneContext _context;

        public CreateModel(NetCoreLibrary.Context.PersoneContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Persona Persona { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Persone.Add(Persona);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
