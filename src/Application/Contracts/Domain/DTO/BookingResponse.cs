using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Contracts.Domain.DTO
{
    public class BookingResponse
    {
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public string Status { get; set; }
        public string TestResult { get; set; }
        public DateTime TestDate { get; set; } 
    }
}