using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApi.DAL;
using MovieApi.Models;
using MovieApi.Services.Models;

namespace MovieApi.Services
{
    public class MovieService : IMovieService
    {
        protected MovieDBContext _context;
        protected IRepository<RoleLookup> _roleLookup;
        protected IRepository<Movie> _movie;
        protected IRepository<CastAndCrew> _castAndCrew;


        public MovieService(MovieDBContext context, IRepository<RoleLookup> roleLookup, IRepository<Movie> movie, IRepository<CastAndCrew> castAndCrew)
        {
            _context = context;
            _roleLookup = roleLookup;
            _movie = movie;
            _castAndCrew = castAndCrew;
        }

        public virtual Movie MovieModelToMovie(MovieModel movie)
        {
            Movie newMovie = new Movie();
            if (movie.MovieId == null) newMovie.MovieId = Guid.NewGuid();
            newMovie.Name = movie.Name;
            newMovie.Plot = movie.Plot;
            newMovie.ReleaseDate = movie.ReleaseDate;

            return newMovie;
        }
        public virtual async Task<Guid> AddMovie(MovieModel movie)
        {
            var newMovie = MovieModelToMovie(movie);
            await _movie.Add(newMovie);
            return newMovie.MovieId;
        }

        public virtual async Task SaveMovie(MovieModel movie, Guid id)
        {
            var newMovie = MovieModelToMovie(movie);
            newMovie.MovieId = id;
            await _movie.Save(newMovie);
        }
    }
}
