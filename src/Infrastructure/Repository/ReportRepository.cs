using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Behaviours;
using Application.Contracts.Domain.DTO;
using Application.Contracts.Repository;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ReportRepository : GenericRepository<LocationDetail>, IReportRepository
    {
        public ReportRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<ReportsResponse>> GetReport()
        {
            try
            {
                List<ReportsResponse> resultList = new();

                var bookings =  _dbcontext.Bookings.AsEnumerable().GroupBy(x => new { x.LocationId }).Select(
                g => 
                    new { g.Key.LocationId, ActualBooking = g.Sum(x => x.Status == StatusModel.Pending ? 1 : 0), 
                        CancelledBooking = g.Sum(x => x.Status == StatusModel.Cancelled ? 1 : 0), 
                        CompletedBooking = g.Sum(x => x.Status == StatusModel.Closed ? 1 : 0),
                        PositiveCases = g.Sum(x => x.TestResult == TestResultModel.Positive ? 1 : 0),
                        NegativeCases = g.Sum(x => x.TestResult == TestResultModel.Negative ? 1 : 0),
                }).ToList();

                foreach (var item in bookings)
                {
                    ReportsResponse result = new();
                    result.LocationName =  _dbcontext.LocationDetails.Where(c => c.Id == item.LocationId).Select(x => x.LocationName).FirstOrDefault();
                    result.Capacity = _dbcontext.LocationDetails.Where(c => c.Id == item.LocationId).Select(x => x.AvailableSpace).FirstOrDefault();
                    result.CreatedAt = _dbcontext.LocationDetails.Where(c => c.Id == item.LocationId).Select(x => x.CreatedAt).FirstOrDefault();
                    result.ActualBooking = item.ActualBooking;
                    result.CancelledBooking = item.CancelledBooking;
                    result.CompletedBooking = item.CompletedBooking;
                    result.PositiveCases = item.PositiveCases;
                    result.NegativeCases = item.NegativeCases;

                    resultList.Add(result);
                }
                return resultList;

            }
            catch (Exception ex)
            {
                throw new HandleDbException(ex.Message);
            }
        }
    }
}