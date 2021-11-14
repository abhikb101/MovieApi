using System;
using System.Collections.Generic;

#nullable disable

namespace MovieApi.Models
{
    public partial class Producer
    {
        public Guid ProducerId { get; set; }
        public Guid PersonId { get; set; }
        public string Company { get; set; }

        public virtual Person Person { get; set; }
    }
}
