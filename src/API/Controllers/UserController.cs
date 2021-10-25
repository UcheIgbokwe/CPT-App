using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;
using Application.Features.Accounts;
using Application.Features.Booking;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
            
        }

        /// <summary>
        /// Create User on the management portal.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("RegisterUser")]
        [ProducesResponseType(typeof(RegisterResponse), statusCode: 201)]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);

            return Created("",result.Response);
        }

        /// <summary>
        /// Create Booking on the management portal.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("CreateBooking")]
        [ProducesResponseType(typeof(BookingResponse), statusCode: 201)]
        public async Task<IActionResult> CreateBooking([FromForm] CreateBookingCommand command)
        {
            var result = await _mediator.Send(command);

            return Created("",result.Response);
        }
    }
}