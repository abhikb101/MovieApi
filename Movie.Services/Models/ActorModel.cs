using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Services.Models
{
    public class ActorModel : PersonModel
    {
        public Guid? ActorId { get; set; }
    }
}
