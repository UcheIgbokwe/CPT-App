using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Behaviours;
using Application.Contracts.Domain.DTO;
using Application.Contracts.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Booking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, CommandResponse>
    {
        private readonly ILogger<CreateBookingCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookingRepository _bookingRepository;
        public CreateBookingCommandHandler(IBookingRepository bookingRepository, IUnitOfWork unitOfWork, ILogger<CreateBookingCommandHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            
        }
        public async Task<CommandResponse> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            try
            {
                CommandResponse response = new();
                BookingResponse bookingResponse = await _bookingRepository.CreateBooking(command);
                await _unitOfWork.CompleteAsync();
                response.Response = bookingResponse ?? throw new HttpStatusException(HttpStatusCode.Conflict, "Error creating booking.");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateBookingCommandHandler: {ex.Message}");
                throw new InvalidResponseException(ex.Message);
            }
        }
    }
}