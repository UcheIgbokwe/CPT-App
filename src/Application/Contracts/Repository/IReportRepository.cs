using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;

namespace Application.Contracts.Repository
{
    public interface IReportRepository
    {
        Task<IEnumerable<ReportsResponse>> GetReport();
    }
}