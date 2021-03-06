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
        private readonly ITicketRepository _ticketRepository;

        public CommentService(
            ILogger<CommentService> logger,
            ICommentRepository commentRepository,
            ITicketRepository ticketRepository)
        {
            _logger = logger;
            _commentRepository = commentRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<(bool isOk, int id)> CreateCommentAsync(Comment comment, CancellationToken cancellationToken = default)
        {
            try
            {
                var ticket = await _ticketRepository.GetTicketAsync(comment.TicketId, cancellationToken);

                if (ticket == null)
                {
                    return (false, -1);
                }

                var result = await _commentRepository.CreateAsync(new Repository.Models.Comment()
                {
                    Description = comment.Description,
                    CreatorId = comment.CreatorId,
                    CreationTime = DateTime.Now,
                    TicketId = comment.TicketId
                }, cancellationToken);

                return (true, result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}