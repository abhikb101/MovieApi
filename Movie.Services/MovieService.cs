using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApi.DAL;
using MovieApi.Models;
using MovieApi.Services.Models;


namespace MovieApi.Services
{
    public class MovieService : IMovieService
    {
        protected MovieDBContext _context;
        protected IRepository<GenderLookup> _genderLookup;
        protected IRepository<RoleLookup> _roleLookup;
        protected IRepository<Movie> _movie;
        protected IRepository<CastAndCrew> _castAndCrew;


        public MovieService(MovieDBContext context, IRepository<RoleLookup> roleLookup, IRepository<Movie> movie, IRepository<CastAndCrew> castAndCrew, IRepository<GenderLookup> genderLookup)
        {
            _context = context;
            _roleLookup = roleLookup;
            _movie = movie;
            _castAndCrew = castAndCrew;
            _genderLookup = genderLookup;
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

        private async Task<ActorModel> ActorToActorModelAsync(Actor actor)
        {
            var ActorDTO = new ActorModel();
            ActorDTO.ActorId = actor.ActorId;
            ActorDTO.Name = actor.Person.Name;
            ActorDTO.Bio = actor.Person.Bio;
            ActorDTO.DateOfBirth = actor.Person.DateOfBirth;
            var gender = await _genderLookup.GetByID(actor.Person.Gender);
            ActorDTO.Gender = gender.Gender;


            return ActorDTO;
        }

        private async Task<ProducerModel> ProducerToProducerModelAsync(Producer producer)
        {
            var producerDTO = new ProducerModel();
            producerDTO.ProducerId = producer.ProducerId;
            producerDTO.Company = producer.Company;
            producerDTO.Name = producer.Person.Name;
            producerDTO.Bio = producer.Person.Bio;
            producerDTO.DateOfBirth = producer.Person.DateOfBirth;
            var gender = await _genderLookup.GetByID(producer.Person.Gender);
            producerDTO.Gender = gender.Gender;

            return producerDTO;
        }

        public virtual async Task<MovieModel> MovieToMovieModel(Movie movie)
        {
            var cast = _context.CastAndCrews.Include(t => t.Person).Where(t => t.MovieId == movie.MovieId).ToList();
            var movieModel = new MovieModel();
            List<Actor> actors = new List<Actor>();
            List<Guid> actorIds = new List<Guid>();
            List<Producer> producers = new List<Producer>();
            List<Guid> producerIds = new List<Guid>();
            foreach(var c in cast)
            {
                if(c.Role == 0)
                {
                    actorIds.Add(c.PersonId);
                }
                else if(c.Role == 1)
                {
                    producerIds.Add(c.PersonId);
                }
            }

            actors = _context.Actors.Where(t => actorIds.Contains(t.PersonId)).Include(t => t.Person).ToList();
            producers = _context.Producers.Where(t => producerIds.Contains(t.PersonId)).Include(t => t.Person).ToList();
            
            List<ActorModel> actorsM = new List<ActorModel>();
            foreach(var a in actors)
            {
                ActorModel ActorDTO = await ActorToActorModelAsync(a);
                actorsM.Add(ActorDTO);
            }

            List<ProducerModel> producersM = new List<ProducerModel>();
            foreach (var p in producers)
            {
                ProducerModel ProducerDTO = await ProducerToProducerModelAsync(p);
                producersM.Add(ProducerDTO);
            }

            movieModel.Actors = actorsM;
            movieModel.Producers = producersM;
            movieModel.Name = movie.Name;
            movieModel.Plot = movie.Plot;
            movieModel.ReleaseDate = movie.ReleaseDate;
            movieModel.MovieId = movie.MovieId;

            return movieModel;

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

        public virtual async Task<List<MovieModel>> GetAllMovies()
        {
            List<Movie> movies = await _movie.GetAll();
            List<MovieModel> movieModels = new List<MovieModel>();
            foreach(var m in movies)
            {
                var mov = await MovieToMovieModel(m);
                movieModels.Add(mov);
            }
            return movieModels;
        }

        public virtual async Task<MovieModel>? GetMovieByID(Guid id)
        {
            var movie = await _movie.GetByID(id);
            if (movie == null) return null;
            return await MovieToMovieModel(movie);
        }
    }
}
