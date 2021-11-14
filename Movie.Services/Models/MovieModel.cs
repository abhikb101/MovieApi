using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Services.Models
{
    public class MovieModel
    {
        public Guid? MovieId;
        public string Name;
        public string Plot;
        public DateTime ReleaseDate;

        List<ActorModel> Actors;
        List<ProducerModel> Producers;
    
        MovieModel()
        {
            Actors = new List<ActorModel>();
            Producers = new List<ProducerModel>();
        }
    }
}
