using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketSystem.API.Constants;
using TicketSystem.API.Filters;
using TicketSystem.API.Services.Interfaces;
using TicketSystem.API.Services.Models;
using TicketSystem.API.ViewModel;
using TicketSystem.Infrastructure;

namespace TicketSystem.API.Controllers.Pm
{
    [ApiController]
    [Area("Pm")]
    [Route("[area]/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly ITicketService _ticketService;

        public TicketController(
            ILogger<TicketController> logger,
            ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        /// <summary>
        /// 新增 Ticket (type = feature)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AuthorizeRoles(Role.Admin, Role.Pm)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TicketInsertViewModel request, CancellationToken cancellationToken = default)
        {
            if (request.Type != TicketType.Feature)
                return BadRequest(BadRequestConstants.TypeIncorrect);

            int.TryParse(User?.Identity?.Name, out var userId);

            var result = await _ticketService.CreateTicketAsync(new Ticket
            {
                Summary = request.Summary,
                Description = request.Description,
                AsigneeUserId = request.AsigneeUserId,
                Priority = request.Priority.HasValue ? (int)request.Priority.Value : default(int?),
                Severity = request.Severity.HasValue ? (int)request.Severity.Value : default(int?),
                Type = (int)request.Type,
                CreatorId = userId,
            }, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// 更新Ticket狀態/處理人
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AuthorizeRoles(Role.Admin, Role.Pm)]
        [HttpPatch]
        public async Task<IActionResult> PatchAsync([FromBody] TicketStatusUpdateViewModel request, CancellationToken cancellationToken = default)
        {
            if (request.Id < 0 || (!request.Status.HasValue && !request.AsigneeUserId.HasValue))
                return BadRequest();

            var origin = await _ticketService.GetTicketAsync(request.Id, cancellationToken);

            if (origin == null)
            {
                return BadRequest(BadRequestConstants.TicketIsNotExist);
            }
    
            if (origin.Type != (int)TicketType.Feature)
                return BadRequest(BadRequestConstants.TypeIncorrect);

            int.TryParse(User?.Identity?.Name, out var userId);

            var status = request.Status.HasValue ? (int)request.Status : default(int?);

            await _ticketService.ModifyTicketStatusOrAsigneeAsync(request.Id, status, request.AsigneeUserId, userId, cancellationToken);

            return Ok();
        }
    }
}