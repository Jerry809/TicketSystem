using System.Threading;
using System.Threading.Tasks;
using TicketSystem.Repository.Models;

namespace TicketSystem.Repository.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<int> CreateAsync(Comment comment, CancellationToken cancellationToken = default);
    }
}