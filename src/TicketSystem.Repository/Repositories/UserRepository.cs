using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Repository.Models;
using TicketSystem.Repository.Repositories.Interfaces;

namespace TicketSystem.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TicketSystemContext _dbContext;

        public UserRepository(TicketSystemContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(User user, CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }

        public async Task<int> Update(User user, CancellationToken cancellationToken = default)
        {
            var origin = _dbContext.Find<User>(user.Id);

            origin.Name = user.Name;
            origin.Role = user.Role;
            origin.IsDisabled = user.IsDisabled;
            origin.UpdateUserId = user.UpdateUserId;
            origin.UpdateTime = DateTime.Now;

            _dbContext.Users.Update(origin);

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> GetUserList(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.AsQueryable().ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<User> GetUser(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}