using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubstanceLogger.Data;
using SubstanceLogger.Models;

namespace SubstanceLogger.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubstanceLogsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SubstanceLogsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/SubstanceLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubstanceLog>>> GetSubstanceLogs()
        {
            var userId = _userManager.GetUserId(User);
            return await _context.SubstanceLogs
                .Include(s => s.Substance)
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.LogDate)
                .ToListAsync();
        }

        // GET: api/SubstanceLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubstanceLog>> GetSubstanceLog(int id)
        {
            var userId = _userManager.GetUserId(User);
            var substanceLog = await _context.SubstanceLogs
                .Include(s => s.Substance)
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

            if (substanceLog == null)
            {
                return NotFound();
            }

            return substanceLog;
        }

        // PUT: api/SubstanceLogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubstanceLog(int id, SubstanceLog substanceLog)
        {
            if (id != substanceLog.Id)
            {
                return BadRequest();
            }

            var userId = _userManager.GetUserId(User);
            var existingLog = await _context.SubstanceLogs.FindAsync(id);
            
            if (existingLog == null || existingLog.UserId != userId)
            {
                return NotFound();
            }

            // Update only allowed fields
            existingLog.SubstanceId = substanceLog.SubstanceId;
            existingLog.LogDate = substanceLog.LogDate;
            existingLog.Amount = substanceLog.Amount;
            existingLog.Unit = substanceLog.Unit;
            existingLog.Notes = substanceLog.Notes;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubstanceLogExists(id))
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

        // POST: api/SubstanceLogs
        [HttpPost]
        public async Task<ActionResult<SubstanceLog>> PostSubstanceLog(SubstanceLog substanceLog)
        {
            substanceLog.UserId = _userManager.GetUserId(User);
            _context.SubstanceLogs.Add(substanceLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubstanceLog", new { id = substanceLog.Id }, substanceLog);
        }

        // DELETE: api/SubstanceLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubstanceLog(int id)
        {
            var userId = _userManager.GetUserId(User);
            var substanceLog = await _context.SubstanceLogs.FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
            
            if (substanceLog == null)
            {
                return NotFound();
            }

            _context.SubstanceLogs.Remove(substanceLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubstanceLogExists(int id)
        {
            return _context.SubstanceLogs.Any(e => e.Id == id);
        }
    }
}
