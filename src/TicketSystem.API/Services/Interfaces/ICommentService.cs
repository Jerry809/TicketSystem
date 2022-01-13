using System.Threading;
using System.Threading.Tasks;
using TicketSystem.API.Services.Models;

namespace TicketSystem.API.Services.Interfaces
{
    public interface ICommentService
    {
        Task<(bool isOk, string message)> CreateCommentAsync(Comment comment, CancellationToken cancellationToken = default);
    }
}