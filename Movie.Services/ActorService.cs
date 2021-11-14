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
    public class ActorService : IActorService
    {
        protected MovieDBContext _context;
        protected IRepository<GenderLookup> _genderLookup;
        protected IRepository<Actor> _actor;


        public ActorService(MovieDBContext context, IRepository<GenderLookup> genderLookup, IRepository<Actor> actor)
        {
            _context = context;
            _genderLookup = genderLookup;
            _actor = actor;
        }

        public virtual async Task<ActorModel> ActorToActorModelAsync(Actor actor)
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


        public virtual async Task<Actor> ActorModelToActorAsync(ActorModel actor, Guid? id = null)
        {
            var gender = await _genderLookup.GetAll();
            Actor newActor;

            if (id == null)
            {
                Guid actorId = Guid.NewGuid();
                Guid personId = Guid.NewGuid();

                newActor = new Actor();
                newActor.ActorId = actorId;
                newActor.PersonId = personId;
                newActor.Person = new Person();
                newActor.Person.PersonId = personId;
            }
            else
            {
                newActor = await _context.Actors.Include(actor => actor.Person).FirstOrDefaultAsync(x => x.ActorId == id);
            }

            newActor.Person.Name = actor.Name;
            newActor.Person.DateOfBirth = actor.DateOfBirth;
            newActor.Person.Bio = actor.Bio;
            newActor.Person.Gender = gender.First(x => x.Gender.ToUpper() == actor.Gender.ToUpper()).GenderId;
            return newActor;
        }
        public virtual async Task<List<ActorModel>> GetAllActors()
        {
            List<Actor> Actors = await _context.Actors.Include(actor => actor.Person).ToListAsync();
            List<ActorModel> ActorsDTO = new List<ActorModel>();
            foreach(var Actor in Actors)
            {

                ActorModel ActorDTO = await ActorToActorModelAsync(Actor);
                ActorsDTO.Add(ActorDTO);
            }
            return ActorsDTO;
        }

        public virtual async Task<ActorModel>? GetActorById(Guid Id)
        {
            Actor Actor = await _context.Actors.Include(actor => actor.Person).FirstOrDefaultAsync(x => x.ActorId == Id);
            if (Actor == null) return null;
            ActorModel ActorDTO = await ActorToActorModelAsync(Actor);

            return ActorDTO;
        }

        public virtual async Task<Guid> AddActor(ActorModel actor)
        {
            var newActor = await ActorModelToActorAsync(actor, null);
            await _actor.Add(newActor);
            return newActor.ActorId;
        }


        public virtual async Task SaveActor(ActorModel actor, Guid id)
        {
            var newActor = await ActorModelToActorAsync(actor, id);
            await _actor.Save(newActor);
        }
    }
}
