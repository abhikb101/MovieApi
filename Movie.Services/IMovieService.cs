using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieApi.Models;
using MovieApi.Services.Models;

namespace MovieApi.Services
{
    public interface IMovieService
    {
        Movie MovieModelToMovie(MovieModel movie);
        Task<MovieModel> MovieToMovieModel(Movie movie);
        Task<Guid> AddMovie(MovieModel movie);
        Task SaveMovie(MovieModel movie, Guid id);
        Task<List<MovieModel>> GetAllMovies();
        Task<MovieModel> GetMovieByID(Guid id);
    }
}