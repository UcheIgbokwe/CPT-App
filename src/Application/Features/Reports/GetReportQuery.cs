using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;
using MediatR;

namespace Application.Features.Reports
{
    public class GetReportQuery : IRequest<IEnumerable<ReportsResponse>>
    {
        
    }
}