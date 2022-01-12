using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketSystem.API.Services.Interfaces;
using TicketSystem.API.Services.Models;
using TicketSystem.Repository.Repositories.Interfaces;

namespace TicketSystem.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<int> CreateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}