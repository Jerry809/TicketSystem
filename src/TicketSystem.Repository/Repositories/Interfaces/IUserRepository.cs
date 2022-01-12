using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketSystem.Repository.Models;

namespace TicketSystem.Repository.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<int> Create(User user, CancellationToken cancellationToken = default);

        Task<int> Update(User user, CancellationToken cancellationToken = default);

        Task<IEnumerable<User>> GetUserList(CancellationToken cancellationToken = default);

        Task<User> GetUser(int id, CancellationToken cancellationToken = default);
    }
}