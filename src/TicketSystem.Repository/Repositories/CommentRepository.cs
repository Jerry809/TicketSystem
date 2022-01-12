using System.Threading;
using System.Threading.Tasks;
using TicketSystem.Repository.Models;
using TicketSystem.Repository.Repositories.Interfaces;

namespace TicketSystem.Repository.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly TicketSystemContext _dbContext;

        public CommentRepository(TicketSystemContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(Comment comment, CancellationToken cancellationToken = default)
        {
            await _dbContext.Comments.AddAsync(comment, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return comment.Id;
        }
        
        // 先不開放修改comment
    }
}