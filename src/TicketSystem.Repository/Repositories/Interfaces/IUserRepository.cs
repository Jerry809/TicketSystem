using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketSystem.Repository.Models;

namespace TicketSystem.Repository.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<int> CreateAsync(User user, CancellationToken cancellationToken = default);

        Task<int> UpdateAsync(User user, CancellationToken cancellationToken = default);

        Task<IEnumerable<User>> GetUserListAsync(CancellationToken cancellationToken = default);

        Task<User> GetUserAsync(int id, CancellationToken cancellationToken = default);

        Task<User> LoginAsync(string account, string password, CancellationToken cancellationToken = default);

        Task<bool> IsExistAccountAsync(string account, CancellationToken cancellationToken = default);
    }
}