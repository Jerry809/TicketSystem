using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketSystem.API.Services.Models;

namespace TicketSystem.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> CreateUserAsync(User user, CancellationToken cancellationToken = default);
        Task<int> UpdateUserAsync(User user, CancellationToken cancellationToken = default);
        Task<User> GetUserAsync(int userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken = default);
    }
}