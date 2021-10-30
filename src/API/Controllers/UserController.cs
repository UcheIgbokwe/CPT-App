using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Behaviours;
using Application.Contracts.Domain.DTO;
using Application.Features.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
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
        /// Returns all Users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), statusCode: 200)]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var result =  await _mediator.Send(new GetUsersQuery());

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