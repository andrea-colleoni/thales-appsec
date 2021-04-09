using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OreLavorateLib.Model;
using OreLavorateLib.Services;

namespace OreLavorateAPI.Controllers
{
    [Route("api/utenti")]
    [ApiController]
    public class UtentiController : ControllerBase
    {
        private readonly IUtenteService _service;
        private readonly ILogger<OreLavorateAPI.Program> _logger;

        public UtentiController(
            IUtenteService service,
            ILogger<OreLavorateAPI.Program> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/Utenti
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Utente>>> GetUtentes()
        {
            _logger.LogInformation($"retrieve all utenti");
            return await _service.all();
        }

        // GET: api/Utenti/5
        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Utente>> GetUtente(string username)
        {
            var utente = await _service.byUsername(username);

            if (utente == null)
            {
                return NotFound();
            }

            return utente;
        }

        // PUT: api/Utenti/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{username}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutUtente(string username, Utente utente)
        {
            if (username != utente.Username)
            {
                return BadRequest();
            }
            if(!(await _service.exists(username)))
            {
                return NotFound();
            }

            await _service.save(utente);

            return NoContent();
        }

        // POST: api/Utenti
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Utente>> PostUtente(Utente utente)
        {
            await _service.save(utente);

            return CreatedAtAction("GetUtente", new { id = utente.Username }, utente);
        }

        // DELETE: api/Utenti/5
        [HttpDelete("{username}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUtente(string username)
        {
            if (!(await _service.exists(username)))
            {
                return NotFound();
            }

            await _service.delete(username);

            return NoContent();
        }
    }
}
