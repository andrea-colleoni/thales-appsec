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
    [Route("api/ore-lavorate")]
    [ApiController]
    public class OreLavorateController : ControllerBase
    {
        private readonly OrelavorateContext _context;
        private readonly ILogger<OreLavorateAPI.Program> _logger;

        public OreLavorateController(
            OrelavorateContext context,
            ILogger<OreLavorateAPI.Program> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/OreLavorate
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OreLavorate>>> GetOreLavorates()
        {
            _logger.LogInformation($"retrieve all ore lavorate");
            return await _context.OreLavorates.ToListAsync();
        }

        // GET: api/OreLavorate/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OreLavorate>> GetOreLavorate(int id)
        {
            var oreLavorate = await _context.OreLavorates.FindAsync(id);

            if (oreLavorate == null)
            {
                return NotFound();
            }

            return oreLavorate;
        }

        // PUT: api/OreLavorate/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutOreLavorate(int id, OreLavorate oreLavorate)
        {
            if (id != oreLavorate.IdOreLavorate)
            {
                return BadRequest();
            }

            _context.Entry(oreLavorate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OreLavorateExists(id))
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

        // POST: api/OreLavorate
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<OreLavorate>> PostOreLavorate(OreLavorate oreLavorate)
        {
            _context.OreLavorates.Add(oreLavorate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOreLavorate", new { id = oreLavorate.IdOreLavorate }, oreLavorate);
        }

        // DELETE: api/OreLavorate/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOreLavorate(int id)
        {
            var oreLavorate = await _context.OreLavorates.FindAsync(id);
            if (oreLavorate == null)
            {
                return NotFound();
            }

            _context.OreLavorates.Remove(oreLavorate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OreLavorateExists(int id)
        {
            return _context.OreLavorates.Any(e => e.IdOreLavorate == id);
        }
    }
}
