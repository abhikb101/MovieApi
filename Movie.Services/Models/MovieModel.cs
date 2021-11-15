using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Services.Models
{
    public class MovieModel
    {
        public Guid? MovieId { get; set; }
        public string Name { get; set; }
        public string Plot { get; set; }
        public DateTime ReleaseDate { get; set; }

        public List<ActorModel> Actors { get; set; }
        public List<ProducerModel> Producers { get; set; }

        public MovieModel()
        {
            Actors = new List<ActorModel>();
            Producers = new List<ProducerModel>();
        }
    }
}
