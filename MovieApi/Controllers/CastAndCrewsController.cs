using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.DAL;
using MovieApi.Models;
using MovieApi.Services;

namespace MovieApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastAndCrewsController : ControllerBase
    {
        private readonly IRepository<CastAndCrew> _context;
        private readonly IRepository<Actor> _actorService;
        private readonly IRepository<Producer> _producerService;
        public CastAndCrewsController(IRepository<CastAndCrew> context, IRepository<Actor> actorService, IRepository<Producer> producerService)
        {
            _context = context;
            _actorService = actorService;
            _producerService = producerService;

        }

        // POST: api/CastAndCrews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CastAndCrew>> AddCast(Guid movieId, Guid actorId, int role)
        {
            var cast = new CastAndCrew();
            cast.MovieId = movieId;
            cast.CrewId = Guid.NewGuid();
            cast.Role = role;
            if (role == 0)
            {
                var actor = await _actorService.GetByID(actorId);

                if(actor == null)
                {
                    return NotFound();
                }

                cast.PersonId = actor.PersonId;
                
            }

            if (role == 1)
            {
                var producer = await _producerService.GetByID(actorId);

                if (producer == null)
                {
                    return NotFound();
                }

                cast.PersonId = producer.PersonId;

            }

            await _context.Add(cast);
            return CreatedAtAction("GetCastAndCrew", new { id = cast.CrewId }, cast);
        }

        // DELETE: api/CastAndCrews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCastAndCrew(Guid movieId, Guid actorId, int role)
        {
            var cast = new List<CastAndCrew>();

            Guid personId = new Guid();
            if (role == 0)
            {
                var actor = await _actorService.GetByID(actorId);

                if (actor == null)
                {
                    return NotFound();
                }

                personId = actor.PersonId;

            }

            if (role == 1)
            {
                var producer = await _producerService.GetByID(actorId);

                if (producer == null)
                {
                    return NotFound();
                }

                personId = producer.PersonId;

            }

            cast  = await _context.Get<CastAndCrew>(t =>  t.MovieId == movieId && t.PersonId == personId);

            await _context.Delete(cast.First().CrewId);

            return NoContent();
        }

    }
}
