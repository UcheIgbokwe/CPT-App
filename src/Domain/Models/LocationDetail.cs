using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class LocationDetail
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public int AvailableSpace { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}