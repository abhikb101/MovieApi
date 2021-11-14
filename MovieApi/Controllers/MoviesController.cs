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
    public class MoviesController : ControllerBase
    {
        private readonly MovieDBContext _movieDB;
        private readonly IMovieService _movieService;
        private readonly IRepository<Movie> _movie;
        private readonly IRepository<CastAndCrew> _castAndCrew;
        private readonly IRepository<RoleLookup> _role;

        public MoviesController(IMovieService movieService, IRepository<Movie> movie, IRepository<CastAndCrew> castAndCrew, IRepository<RoleLookup> role)
        {
            _movieService = movieService;
            _movie = movie;
            _castAndCrew = castAndCrew;
            _role = role;

        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieModel>>> GetMovies()
        {
            return await _movieService.GetAllMovies();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieModel>> GetMovie(Guid id)
        {
            var movie = await _movieService.GetMovieByID(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(Guid id, MovieModel movie)
        {
            if (id != movie.MovieId)
            {
                return BadRequest();
            }

            try
            {
                await _movieService.SaveMovie(movie, id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await MovieExistsAsync(id)))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieModel movie)
        {
            Guid movieId = new Guid();
            try
            {
                movieId = await _movieService.AddMovie(movie);
                movie.MovieId = movieId;
            }
            catch (DbUpdateException)
            {
                if (await MovieExistsAsync(movie.MovieId.Value))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            var movie = await _movie.GetByID(id);
            if (movie == null)
            {
                return NotFound();
            }

            await _movie.Delete(id);
            return NoContent();
        }

        private async Task<bool> MovieExistsAsync(Guid id)
        {
            var movie = await _movie.GetByID(id);
            return movie == null ? false : true;
        }
    }
}
