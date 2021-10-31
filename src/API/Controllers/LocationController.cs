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
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
            
        }

        /// <summary>
        /// Create Spaces on the management portal.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateSpaces([FromBody] CreateSpacesCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return Created("", result);
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
        /// Update Spaces on the management portal.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateSpaces([FromBody] UpdateSpacesCommand command)
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
        /// Returns a location.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result =  await _mediator.Send(new GetLocationQuery(id));

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