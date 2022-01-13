using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketSystem.API.Services.Interfaces;
using TicketSystem.API.Services.Models;
using TicketSystem.API.ViewModel;

namespace TicketSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(
            ILogger<CommentController> logger,
            ICommentService commentService,
            IMapper mapper)
        {
            _logger = logger;
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CommentInsertViewModel request, CancellationToken cancellationToken = default)
        {
            var comment = _mapper.Map<Comment>(request);

            int.TryParse(User?.Identity?.Name, out var userId);
            comment.CreatorId = userId;
            
            await _commentService.CreateCommentAsync(comment, cancellationToken);

            return Ok();
        }
    }
}