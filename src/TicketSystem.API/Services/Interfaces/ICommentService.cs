using System.Threading;
using System.Threading.Tasks;
using TicketSystem.API.Services.Models;

namespace TicketSystem.API.Services.Interfaces
{
    public interface ICommentService
    {
        Task<(bool isOk,  int id)> CreateCommentAsync(Comment comment, CancellationToken cancellationToken = default);
    }
}