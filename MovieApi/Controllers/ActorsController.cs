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
using MovieApi.Services.Models;

namespace MovieApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IRepository<Actor> _context;
        private readonly IActorService _actorService;

        public ActorsController(IRepository<Actor> context, IActorService actorService)
        {
            _context = context;
            _actorService = actorService;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorModel>>> GetActors()
        {
            return await _actorService.GetAllActors();
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorModel>> GetActor(Guid id)
        {
            var actor = await _actorService.GetActorById(id);

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(Guid id, ActorModel actor)
        {
            if (id != actor.ActorId)
            {
                return BadRequest();
            }

            

            try
            {
                await _actorService.SaveActor(actor, id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await ActorExistsAsync(id)))
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

        // POST: api/Actors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor(ActorModel actor)
        {
            Guid actorID = new Guid();

            try
            {
                actorID = await _actorService.AddActor(actor);
                actor.ActorId = actorID;
            }
            catch (Exception ex)
            {
                if (await ActorExistsAsync(actorID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetActor", new { id = actorID }, actor);
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(Guid id)
        {
            var actor = await _context.GetByID(id);
            if (actor == null)
            {
                return NotFound();
            }

            await _context.Delete(id);

            return NoContent();
        }

        private async Task<bool> ActorExistsAsync(Guid id)
        {
            var actor = await _context.GetByID(id);
            return actor == null ? false : true;
        }
    }
}
