using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketSystem.API.Services.Interfaces;
using TicketSystem.API.Services.Models;
using TicketSystem.API.ViewModel;
using TicketSystem.API.ViewModel.Validators;
using TicketSystem.Infrastructure;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketViewModel))]
        public async Task<IActionResult> GetAsync([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var ticket = await _ticketService.GetTicketAsync(id, cancellationToken);

            return ticket == null
                ? NotFound()
                : Ok(new TicketViewModel
                {
                    Id = ticket.Id,
                    Description = ticket.Description,
                    Priority = ticket.Priority.HasValue
                        ? (Priority)ticket.Priority.Value
                        : default(Priority?),
                    Severity = ticket.Severity.HasValue
                        ? (TicketSeverity)ticket.Severity.Value
                        : default(TicketSeverity?),
                    Summary = ticket.Summary,
                    Status = (TicketStatus)ticket.Status,
                    Type = (TicketType)ticket.Type,
                    AsigneeUserId = ticket.AsigneeUserId,
                    CreatorId = ticket.CreatorId,
                    CreationTime = ticket.CreationTime,
                    UpdateTime = ticket.UpdateTime,
                    UpdateId = ticket.UpdateId,
                    Comments = ticket.Comments.Select(s => new CommentViewModel
                    {
                        CreatorId = s.CreatorId,
                        Description = s.Description,
                        Id = s.Id,
                        TicketId = s.TicketId,
                        CreationTime = s.CreationTime,
                    })
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TicketViewModel>))]
        public async Task<IActionResult> GetTicketsAsync(
            [FromQuery] TicketFilter filter,
            CancellationToken cancellationToken = default)
        {
            var ticket = await _ticketService.GetTicketsAsync(filter, cancellationToken);

            return Ok(ticket.Select(s => new TicketViewModel()
            {
                Id = s.Id,
                Description = s.Description,
                Priority = s.Priority.HasValue
                    ? (Priority)s.Priority.Value
                    : default(Priority?),
                Severity = s.Severity.HasValue
                    ? (TicketSeverity)s.Severity.Value
                    : default(TicketSeverity?),
                Summary = s.Summary,
                Status = (TicketStatus)s.Status,
                Type = (TicketType)s.Type,
                AsigneeUserId = s.AsigneeUserId,
                CreatorId = s.CreatorId,
                CreationTime = s.CreationTime,
                UpdateTime = s.UpdateTime,
                UpdateId = s.UpdateId,
            }));
        }
    }
}