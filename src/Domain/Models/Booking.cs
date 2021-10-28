using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int LocationId { get; set; }
        public string Status { get; set; } = StatusModel.Pending;
        public string TestType { get; set; }
        public string TestResult { get; set; }
        public DateTime TestDate { get; set; } 
        public DateTime UpdateDate { get; set; } 
    }
}