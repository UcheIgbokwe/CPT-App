using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Contracts.Domain.DTO
{
    public class LocationResponse
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public int AvailableSpace { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class LocationResponse2
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public int AvailableSpace { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}