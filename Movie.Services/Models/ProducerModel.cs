using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Services.Models
{
    public class ProducerModel : PersonModel
    {
        public Guid? ProducerId { get; set; }
        public string Company { get; set; }
    }
}
