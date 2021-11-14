using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.DAL;
using MovieApi.Models;

namespace MovieApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastAndCrewsController : ControllerBase
    {
        private readonly MovieDBContext _context;

        public CastAndCrewsController(MovieDBContext context)
        {
            _context = context;
        }

        // GET: api/CastAndCrews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CastAndCrew>>> GetCastAndCrews()
        {
            return await _context.CastAndCrews.ToListAsync();
        }

        // GET: api/CastAndCrews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CastAndCrew>> GetCastAndCrew(Guid id)
        {
            var castAndCrew = await _context.CastAndCrews.FindAsync(id);

            if (castAndCrew == null)
            {
                return NotFound();
            }

            return castAndCrew;
        }

        // PUT: api/CastAndCrews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCastAndCrew(Guid id, CastAndCrew castAndCrew)
        {
            if (id != castAndCrew.CrewId)
            {
                return BadRequest();
            }

            _context.Entry(castAndCrew).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CastAndCrewExists(id))
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

        // POST: api/CastAndCrews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CastAndCrew>> PostCastAndCrew(CastAndCrew castAndCrew)
        {
            _context.CastAndCrews.Add(castAndCrew);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CastAndCrewExists(castAndCrew.CrewId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCastAndCrew", new { id = castAndCrew.CrewId }, castAndCrew);
        }

        // DELETE: api/CastAndCrews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCastAndCrew(Guid id)
        {
            var castAndCrew = await _context.CastAndCrews.FindAsync(id);
            if (castAndCrew == null)
            {
                return NotFound();
            }

            _context.CastAndCrews.Remove(castAndCrew);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CastAndCrewExists(Guid id)
        {
            return _context.CastAndCrews.Any(e => e.CrewId == id);
        }
    }
}
