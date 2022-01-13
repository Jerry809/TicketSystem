using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketSystem.API.Extensions;
using TicketSystem.API.Helpers;
using TicketSystem.API.Services.Interfaces;
using TicketSystem.API.Services.Models;
using TicketSystem.Repository.Repositories.Interfaces;

namespace TicketSystem.API.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly JwtHelper _jwtHelper;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, JwtHelper jwtHelper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<int> CreateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            var isExistAccount = await _userRepository.IsExistAccountAsync(user.Account, cancellationToken);

            if (isExistAccount)
            {
                return -1;
            }
            
            var result = await _userRepository.CreateAsync(new Repository.Models.User
            {
                Account = user.Account,
                Password = PasswordHashExtension.GetHashPassword(user.Password),
                Name = user.Name,
                Role = user.Role,
                CreatorId = user.CreatorId,
                CreationTime = DateTime.Now,
                IsDisabled = false,
            }, cancellationToken);

            return result;
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

        public async Task<(bool isOk, string jwt)> LoginAsync(string account, string password, CancellationToken cancellationToken = default)
        {
            var hashPassword = PasswordHashExtension.GetHashPassword(password);
            var user = await _userRepository.LoginAsync(account, hashPassword, cancellationToken);

            return user == null ? (false, string.Empty) : (true, _jwtHelper.GenerateToken(user.Id, user.Role));
        }
    }
}