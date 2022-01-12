using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketSystem.API.Services.Interfaces;
using TicketSystem.API.Services.Models;
using TicketSystem.Repository.Repositories.Interfaces;

namespace TicketSystem.API.Services
{
    public class CommentService : ICommentService
    {
        private readonly ILogger<CommentService> _logger;
        private readonly ICommentRepository _commentRepository;

        public CommentService(ILogger<CommentService> logger, ICommentRepository commentRepository)
        {
            _logger = logger;
            _commentRepository = commentRepository;
        }

        public async Task CreateCommentAsync(Comment comment, CancellationToken cancellationToken = default)
        {
            try
            {
                await _commentRepository.CreateAsync(new Repository.Models.Comment()
                {
                    Description = comment.Description,
                    CreatorId = comment.CreatorId,
                    TicketId = comment.TicketId
                }, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}