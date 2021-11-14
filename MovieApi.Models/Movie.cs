using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApi.Models
{
    public partial class Movie
    {
        public Movie()
        {
            CastAndCrews = new HashSet<CastAndCrew>();
        }

        public Guid MovieId { get; set; }
        public string Name { get; set; }
        public string Plot { get; set; }
        public DateTime ReleaseDate { get; set; }

        public virtual ICollection<CastAndCrew> CastAndCrews { get; set; }
    }
}
