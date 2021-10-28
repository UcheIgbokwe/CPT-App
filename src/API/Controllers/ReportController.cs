using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Behaviours;
using Application.Contracts.Domain.DTO;
using Application.Features.Reports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all available report on bookings.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetData")]
        [ProducesResponseType(typeof(IEnumerable<ReportsResponse>), statusCode: 200)]
        public async Task<IActionResult> GetData()
        {
            try
            {
                var result =  await _mediator.Send(new GetReportQuery());

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