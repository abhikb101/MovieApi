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
    public class ProducerService : IProducerService
    {
        protected MovieDBContext _context;
        protected IRepository<GenderLookup> _genderLookup;
        protected IRepository<Producer> _producer;


        public ProducerService(MovieDBContext context, IRepository<GenderLookup> genderLookup, IRepository<Producer> producer)
        {
            _context = context;
            _genderLookup = genderLookup;
            _producer = producer;
        }

        public virtual async Task<ProducerModel> ProducerToProducerModelAsync(Producer producer)
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


        public virtual async Task<Producer> ProducerModelToProducerAsync(ProducerModel producer, Guid? id = null)
        {
            var gender = await _genderLookup.GetAll();
            Producer newproducer;

            if (id == null)
            {
                Guid producerId = Guid.NewGuid();
                Guid personId = Guid.NewGuid();

                newproducer = new Producer();
                newproducer.ProducerId = producerId;
                newproducer.PersonId = personId;
                newproducer.Person = new Person();
                newproducer.Person.PersonId = personId;
            }
            else
            {
                newproducer = await _context.Producers.Include(producer => producer.Person).FirstOrDefaultAsync(x => x.ProducerId == id);
            }

            newproducer.Company = producer.Company;
            newproducer.Person.Name = producer.Name;
            newproducer.Person.DateOfBirth = producer.DateOfBirth;
            newproducer.Person.Bio = producer.Bio;
            newproducer.Person.Gender = gender.First(x => x.Gender.ToUpper() == producer.Gender.ToUpper()).GenderId;
            return newproducer;
        }
        public virtual async Task<List<ProducerModel>> GetAllProducers()
        {
            List<Producer> producers = await _context.Producers.Include(producer => producer.Person).ToListAsync();
            List<ProducerModel> producersDTO = new List<ProducerModel>();
            foreach(var producer in producers)
            {

                ProducerModel producerDTO = await ProducerToProducerModelAsync(producer);
                producersDTO.Add(producerDTO);
            }
            return producersDTO;
        }

        public virtual async Task<ProducerModel>? GetProducerById(Guid Id)
        {
            Producer producer = await _context.Producers.Include(producer => producer.Person).FirstOrDefaultAsync(x => x.ProducerId == Id);
            if (producer == null) return null;
            ProducerModel producerDTO = await ProducerToProducerModelAsync(producer);

            return producerDTO;
        }

        public virtual async Task<Guid> AddProducer(ProducerModel producer)
        {
            var newproducer = await ProducerModelToProducerAsync(producer, null);
            await _producer.Add(newproducer);
            return newproducer.ProducerId;
        }


        public virtual async Task SaveProducer(ProducerModel producer, Guid id)
        {
            var newproducer = await ProducerModelToProducerAsync(producer, id);
            await _producer.Save(newproducer);
        }
    }
}
