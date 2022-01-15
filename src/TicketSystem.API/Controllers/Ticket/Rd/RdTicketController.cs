using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketSystem.API.Constants;
using TicketSystem.API.Filters;
using TicketSystem.API.Services.Interfaces;
using TicketSystem.API.ViewModel;
using TicketSystem.Infrastructure;

namespace TicketSystem.API.Controllers.Rd
{
    [ApiController]
    [Area("Rd")]
    [Route("[area]/[controller]")]
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
        /// 更新Ticket狀態/處理人
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AuthorizeRoles(Role.Admin, Role.Rd)]
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
            
            if (origin.Type == (int)TicketType.TestCase)
            {
                return BadRequest(BadRequestConstants.TypeIncorrect);
            }

            int.TryParse(User?.Identity?.Name, out var userId);
            var status = request.Status.HasValue ? (int)request.Status : default(int?);

            await _ticketService.ModifyTicketStatusOrAsigneeAsync(request.Id, status, request.AsigneeUserId, userId, cancellationToken);

            return Ok();
        }
    }
}