using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubstanceLogger.Data;
using SubstanceLogger.Models;

namespace SubstanceLogger.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubstancesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubstancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Substances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Substance>>> GetSubstances()
        {
            return await _context.Substances.ToListAsync();
        }

        // GET: api/Substances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Substance>> GetSubstance(int id)
        {
            var substance = await _context.Substances.FindAsync(id);

            if (substance == null)
            {
                return NotFound();
            }

            return substance;
        }

        // PUT: api/Substances/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubstance(int id, Substance substance)
        {
            if (id != substance.Id)
            {
                return BadRequest();
            }

            _context.Entry(substance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubstanceExists(id))
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

        // POST: api/Substances
        [HttpPost]
        public async Task<ActionResult<Substance>> PostSubstance(Substance substance)
        {
            _context.Substances.Add(substance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubstance", new { id = substance.Id }, substance);
        }

        // DELETE: api/Substances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubstance(int id)
        {
            var substance = await _context.Substances.FindAsync(id);
            if (substance == null)
            {
                return NotFound();
            }

            _context.Substances.Remove(substance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubstanceExists(int id)
        {
            return _context.Substances.Any(e => e.Id == id);
        }
    }
}
