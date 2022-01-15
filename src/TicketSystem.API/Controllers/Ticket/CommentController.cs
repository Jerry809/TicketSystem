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

namespace TicketSystem.API.Controllers
{
    [ApiController]
    [Area("Ticket")]
    [Route("[area]/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentService _commentService;

        public CommentController(
            ILogger<CommentController> logger,
            ICommentService commentService)
        {
            _logger = logger;
            _commentService = commentService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AuthorizeRoles(Role.Admin, Role.Pm, Role.Qa, Role.Rd)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CommentInsertViewModel request, CancellationToken cancellationToken = default)
        {
            int.TryParse(User?.Identity?.Name, out var userId);

            var result = await _commentService.CreateCommentAsync(new Comment
            {
                CreatorId = userId,
                Description = request.Description,
                TicketId = request.TicketId
            }, cancellationToken);

            if (!result.isOk)
                return BadRequest(BadRequestConstants.TicketIsNotExist);
            
            return Ok(result.id);
        }
    }
}