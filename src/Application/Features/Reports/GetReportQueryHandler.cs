using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Behaviours;
using Application.Contracts.Domain.DTO;
using Application.Contracts.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Reports
{
    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, IEnumerable<ReportsResponse>>
    {
        private readonly ILogger<GetReportQueryHandler> _logger;
        private readonly IReportRepository _reportRepository;
        public GetReportQueryHandler(IReportRepository reportRepository, ILogger<GetReportQueryHandler> logger)
        {
            _reportRepository = reportRepository;
            _logger = logger;
            
        }
        public async Task<IEnumerable<ReportsResponse>> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _reportRepository.GetReport();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetReportQueryHandler: {ex.Message}");
                throw new InvalidResponseException(ex.Message);
            }
        }
    }
}