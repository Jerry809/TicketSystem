using System.Threading;
using System.Threading.Tasks;
using TicketSystem.API.Services.Models;

namespace TicketSystem.API.Services.Interfaces
{
    public interface ICommentService
    {
        Task CreateCommentAsync(Comment comment, CancellationToken cancellationToken = default);
    }
}