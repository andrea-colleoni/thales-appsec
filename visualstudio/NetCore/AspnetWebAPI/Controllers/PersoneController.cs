using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCoreLibrary.Context;
using NetCoreLibrary.Model;

namespace AspnetWebAPI.Controllers
{
    [Route("api/persone")]
    [ApiController]
    public class PersoneController : ControllerBase
    {
        private readonly PersoneContext _context;
        private readonly ILogger<PersoneController> _logger;

        public PersoneController(PersoneContext context, ILogger<PersoneController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("fisso/{par1}/abcd/{par2}")]
        public ActionResult<bool> TestParametri(string par3, int par2, string par1, Persona p)
        {
            _logger.LogInformation($"par1: {par1}, par2: {par2}, par3: {par3}");
            return Ok(true);
        }

        /// <summary>
        /// Restituisce l'elenco di tutte le persone 
        /// </summary>
        /// <returns>Elenco delle persone</returns>
        [HttpGet]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersone()
        {
            return Ok(await _context.Persone.ToListAsync());
        }

        /// <summary>
        /// Restituisce una persona data l'email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Persona>> GetPersona(string email)
        {
            var persona = await _context.Persone.FindAsync(email);

            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }

        // PUT: api/Persone/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{email}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPersona(string email, Persona persona)
        {
            if (email != persona.Email)
            {
                return BadRequest();
            }

            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(email))
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

        // POST: api/Persone
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Persona>> PostPersona(Persona persona)
        {
            _context.Persone.Add(persona);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PersonaExists(persona.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPersona", new { id = persona.Email }, persona);
        }

        // DELETE: api/Persone/5
        [HttpDelete("{email}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePersona(string email)
        {
            var persona = await _context.Persone.FindAsync(email);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Persone.Remove(persona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaExists(string id)
        {
            return _context.Persone.Any(e => e.Email == id);
        }
    }
}
