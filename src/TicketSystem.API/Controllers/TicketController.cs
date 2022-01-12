using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketSystem.API.Services.Interfaces;
using TicketSystem.API.Services.Models;

namespace TicketSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly ITicketService _ticketService;

        public TicketController(ILogger<TicketController> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Ticket ticket, CancellationToken cancellationToken = default)
        {
            await _ticketService.CreateTicketAsync(ticket, cancellationToken);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Ticket ticket, CancellationToken cancellationToken = default)
        {
            await _ticketService.UpdateTicketAsync(ticket, cancellationToken);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var result = await _ticketService.GetTicketAsync(id, cancellationToken);

            return result == null
                ? NotFound()
                : Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTicketsAsync(
            [FromQuery] TicketFilter filter,
            CancellationToken cancellationToken = default)
        {
            var result = await _ticketService.GetTicketsAsync(filter, cancellationToken);

            return Ok(result);
        }
    }
}