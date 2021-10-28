using System;
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
        /// Create Booking on the management portal.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("CreateBooking")]
        [ProducesResponseType(typeof(BookingResponse), statusCode: 201)]
        public async Task<IActionResult> CreateBooking([FromForm] CreateBookingCommand command)
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
        public async Task<IActionResult> CancelBooking([FromForm] CancelBookingCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return NoContent();
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
        public async Task<IActionResult> UpdateTest([FromForm] UpdateTestCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return NoContent();
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