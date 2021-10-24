using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;
using Application.Features.Accounts;
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
        [ProducesResponseType(typeof(RegisterResponse), statusCode: 200)]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result.Response);
        }
    }
}