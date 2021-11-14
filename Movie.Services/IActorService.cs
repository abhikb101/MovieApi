using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieApi.Models;
using MovieApi.Services.Models;

namespace MovieApi.Services
{
    public interface IActorService
    {
        Task<List<ActorModel>> GetAllActors();
        Task<ActorModel> GetActorById(Guid Id);
        Task SaveActor(ActorModel actor, Guid id);
        Task<Guid> AddActor(ActorModel actor);
    }
}