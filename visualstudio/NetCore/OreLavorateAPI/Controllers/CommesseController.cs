using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OreLavorateLib.Context;
using OreLavorateLib.Model;

namespace OreLavorateAPI.Controllers
{
    [Route("api/commesse")]
    [ApiController]
    public class CommesseController : ControllerBase
    {
        private readonly OrelavorateContext _context;
        private readonly ILogger<OreLavorateAPI.Program> _logger;

        public CommesseController(
            OrelavorateContext context,
            ILogger<OreLavorateAPI.Program> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Commesse
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Commessa>>> GetCommessas()
        {
            _logger.LogInformation($"retrieve all commesse");
            return await _context.Commessas.ToListAsync();
        }

        // GET: api/Commesse/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Commessa>> GetCommessa(int id)
        {
            _logger.LogInformation($"retrieve commessa n. {id}");
            var commessa = await _context.Commessas.FindAsync(id);

            if (commessa == null)
            {
                return NotFound();
            }

            return commessa;
        }

        // PUT: api/Commesse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutCommessa(int id, Commessa commessa)
        {
            _logger.LogInformation($"update commessa n {id}");
            if (id != commessa.IdCommessa)
            {
                return BadRequest();
            }

            _context.Entry(commessa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommessaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Commesse
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Commessa>> PostCommessa(Commessa commessa)
        {
            _logger.LogInformation($"add new commessa");
            _context.Commessas.Add(commessa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommessa", new { id = commessa.IdCommessa }, commessa);
        }

        // DELETE: api/Commesse/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCommessa(int id)
        {
            _logger.LogInformation($"delete commessa n {id}");
            var commessa = await _context.Commessas.FindAsync(id);
            if (commessa == null)
            {
                return NotFound();
            }

            _context.Commessas.Remove(commessa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommessaExists(int id)
        {
            return _context.Commessas.Any(e => e.IdCommessa == id);
        }
    }
}
