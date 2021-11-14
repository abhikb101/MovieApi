using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApi.Models
{
    public partial class Actor
    {
        public Guid ActorId { get; set; }
        public Guid PersonId { get; set; }

        public virtual Person Person { get; set; }
    }
}
