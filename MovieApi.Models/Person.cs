using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApi.Models
{
    public partial class Person
    {
        public Person()
        {
            CastAndCrews = new HashSet<CastAndCrew>();
        }

        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual Producer Producer { get; set; }
        public virtual ICollection<CastAndCrew> CastAndCrews { get; set; }
    }
}
