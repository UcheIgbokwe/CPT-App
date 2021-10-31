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

namespace Application.Features.Booking
{
    public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand, BookingResponseII>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateTestCommandHandler> _logger;
        private readonly IBookingRepository _bookingRepository;
        public UpdateTestCommandHandler(IBookingRepository bookingRepository, IUnitOfWork unitOfWork, ILogger<UpdateTestCommandHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            
        }
        public async Task<BookingResponseII> Handle(UpdateTestCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var locationResponse =  await _bookingRepository.UpdateTestResult(command);
                await _unitOfWork.CompleteAsync();
                return locationResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateTestCommandHandler: {ex.Message}");
                throw new InvalidResponseException(ex.Message);
            }
        }
    }
}