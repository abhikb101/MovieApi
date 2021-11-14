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
    public class ProducersController : ControllerBase
    {
        private readonly IRepository<Producer> _context;
        private readonly IProducerService _producerService;

        public ProducersController(IRepository<Producer> context, IProducerService producerService)
        {
            _context = context;
            _producerService = producerService;
        }

        // GET: api/Producers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProducerModel>>> GetProducers()
        {
            return await _producerService.GetAllProducers();
        }

        // GET: api/Producers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProducerModel>> GetProducer(Guid id)
        {
            var producer = await _producerService.GetProducerById(id);

            if (producer == null)
            {
                return NotFound();
            }

            return producer;
        }

        // PUT: api/Producers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducer(Guid id, ProducerModel producer)
        {
            if (id != producer.ProducerId)
            {
                return BadRequest();
            }

            

            try
            {
                await _producerService.SaveProducer(producer, id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await ProducerExistsAsync(id)))
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

        // POST: api/Producers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producer>> PostProducer(ProducerModel producer)
        {
            Guid producerID = new Guid();

            try
            {
                producerID = await _producerService.AddProducer(producer);
                producer.ProducerId = producerID;
            }
            catch (Exception ex)
            {
                if (await ProducerExistsAsync(producerID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProducer", new { id = producerID }, producer);
        }

        // DELETE: api/Producers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducer(Guid id)
        {
            var producer = await _context.GetByID(id);
            if (producer == null)
            {
                return NotFound();
            }

            await _context.Delete(id);

            return NoContent();
        }

        private async Task<bool> ProducerExistsAsync(Guid id)
        {
            var producer = await _context.GetByID(id);
            return producer == null ? false : true;
        }
    }
}
