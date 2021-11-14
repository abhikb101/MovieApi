using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApi.Models
{
    public partial class CastAndCrew
    {
        public Guid CrewId { get; set; }
        public Guid MovieId { get; set; }
        public Guid PersonId { get; set; }
        public int Role { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Person Person { get; set; }
    }
}
