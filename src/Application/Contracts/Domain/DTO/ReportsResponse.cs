using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Contracts.Domain.DTO
{
    public class ReportsResponse
    {
        public string LocationName { get; set; }
        public int Capacity { get; set; }
        public string CreatedAt { get; set; } 
        public int ActualBooking { get; set; }
        public int CancelledBooking { get; set; }
        public int CompletedBooking { get; set; }
        public int PositiveCases { get; set; }
        public int NegativeCases { get; set; }
    }
}