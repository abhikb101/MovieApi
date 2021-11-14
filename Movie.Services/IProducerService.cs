using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieApi.Models;
using MovieApi.Services.Models;

namespace MovieApi.Services
{
    public interface IProducerService
    {
        Task<List<ProducerModel>> GetAllProducers();
        Task<ProducerModel> GetProducerById(Guid Id);
        Task SaveProducer(ProducerModel actor, Guid id);
        Task<Guid> AddProducer(ProducerModel actor);
    }
}