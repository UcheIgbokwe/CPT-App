using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Behaviours;
using Application.Contracts.Domain.DTO;
using Application.Features.Booking;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
            
        }


        /// <summary>
        /// Returns all Bookings.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookingResponseII>), statusCode: 200)]
        public async Task<IActionResult> GetAllBookings()
        {
            try
            {
                var result =  await _mediator.Send(new GetBookingsQuery());

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is HttpStatusException httpException)
                {
                    return StatusCode((int) httpException.Status, httpException.Message);
                }else{
                    return BadRequest(ex.Message);
                }
            }
            
        }

        /// <summary>
        /// Returns a booking.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            try
            {
                
                var result =  await _mediator.Send(new GetBookingQuery(email));

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is HttpStatusException httpException)
                {
                    return StatusCode((int) httpException.Status, httpException.Message);
                }else{
                    return BadRequest(ex.Message);
                }
            }
            
        } 

        /// <summary>
        /// Create Booking on the management portal.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("CreateBooking")]
        [ProducesResponseType(typeof(BookingResponse), statusCode: 201)]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return Created("",result.Response);
            }
            catch (Exception ex)
            {
                if (ex is HttpStatusException httpException)
                {
                    return StatusCode((int) httpException.Status, httpException.Message);
                }else{
                    return BadRequest(ex.Message);
                }
            }
        }

        /// <summary>
        /// Cancel Booking on the management portal.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("CancelBooking")]
        public async Task<IActionResult> CancelBooking([FromBody] CancelBookingCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is HttpStatusException httpException)
                {
                    return StatusCode((int) httpException.Status, httpException.Message);
                }else{
                    return BadRequest(ex.Message);
                }
            }
        }

        /// <summary>
        /// Update Test record on the management portal.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("UpdateTest")]
        public async Task<IActionResult> UpdateTest([FromBody] UpdateTestCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is HttpStatusException httpException)
                {
                    return StatusCode((int) httpException.Status, httpException.Message);
                }else{
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}