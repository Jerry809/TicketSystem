using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Infrastructure;
using TicketSystem.Repository.Extensions;
using TicketSystem.Repository.Models;
using TicketSystem.Repository.Models.Filters;
using TicketSystem.Repository.Repositories.Interfaces;

namespace TicketSystem.Repository.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketSystemContext _dbContext;

        public TicketRepository(TicketSystemContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(Ticket ticket, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(ticket, cancellationToken);

            var ticketHistory = new TicketHistory
            {
                AsigneeUserId = ticket.AsigneeUserId,
                CreationTime = ticket.CreationTime,
                CreatorId = ticket.CreatorId,
                Description = ticket.Description,
                Priority = ticket.Priority,
                Severity = ticket.Severity,
                Status = ticket.Status,
                Summary = ticket.Summary,
                Type = ticket.Type,
            };

            await _dbContext.TicketHistories.AddAsync(ticketHistory, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return ticket.Id;
        }

        public async Task<int> UpdateAsync(Ticket ticket, CancellationToken cancellationToken = default)
        {
            var origin = _dbContext.Find<Ticket>(ticket.Id);

            origin.Summary = ticket.Summary;
            origin.Description = ticket.Summary;
            origin.Priority = ticket.Priority;
            origin.Severity = ticket.Severity;
            origin.Status = ticket.Status;
            origin.AsigneeUserId = ticket.AsigneeUserId;
            origin.UpdateTime = ticket.UpdateTime;
            origin.UpdateUserId = ticket.UpdateUserId;

            _dbContext.Update(origin);

            await CreateHistory(origin, cancellationToken);

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Ticket>> GetTicketListAsync(GetTicketListFilter filter, CancellationToken cancellationToken = default)
        {
            Expression<Func<Ticket, bool>> predicate = x => true;

            if (filter.Id.HasValue)
            {
                predicate = predicate.And(x => x.Id == filter.Id.Value);
            }

            if (filter.TicketType.HasValue)
            {
                predicate = predicate.And(x => x.Type == filter.TicketType.Value);
            }

            if (filter.AsigneeUserId.HasValue)
            {
                predicate = predicate.And(x => x.AsigneeUserId == filter.AsigneeUserId.Value);
            }

            var ticketQueryable = _dbContext.Tickets.Where(predicate).OrderByDescending(x => x.CreationTime);

            var orderByColumn = typeof(Ticket).GetProperties()
                .FirstOrDefault(x => string.Equals(x.Name, filter.OrderBy, StringComparison.CurrentCultureIgnoreCase))?
                .Name;

            if (!string.IsNullOrWhiteSpace(orderByColumn))
            {
                ticketQueryable = filter.OrderType switch
                {
                    OrderType.AESC => ticketQueryable.ThenBy(orderByColumn),
                    OrderType.DESC => ticketQueryable.ThenByDescending(orderByColumn),
                    _ => ticketQueryable.ThenByDescending(orderByColumn)
                };
            }

            return await ticketQueryable
                .Where(x => !x.IsDeleted)
                .Include(x => x.Comments)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Ticket> GetTicketAsync(int ticketId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Tickets
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == ticketId && !x.IsDeleted, cancellationToken);
        }

        public async Task UpdateStatusAsync(int id, int status, int updateUserId, CancellationToken cancellationToken = default)
        {
            var origin = _dbContext.Find<Ticket>(id);
            origin.Status = status;
            origin.UpdateUserId = updateUserId;
            origin.UpdateTime = DateTime.Now;

            _dbContext.Tickets.Update(origin);

            await CreateHistory(origin, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsigneeAsync(int id, int asigneeUserId, int updateUserId, CancellationToken cancellationToken = default)
        {
            var origin = _dbContext.Find<Ticket>(id);
            origin.AsigneeUserId = asigneeUserId;
            origin.UpdateUserId = updateUserId;
            origin.UpdateTime = DateTime.Now;

            _dbContext.Tickets.Update(origin);

            await CreateHistory(origin, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateStatusAndAsigneeAsync(int id, int status, int asigneeUserId, int updateUserId,
            CancellationToken cancellationToken = default)
        {
            var origin = _dbContext.Find<Ticket>(id);
            origin.AsigneeUserId = asigneeUserId;
            origin.Status = status;
            origin.UpdateUserId = updateUserId;
            origin.UpdateTime = DateTime.Now;

            _dbContext.Tickets.Update(origin);

            await CreateHistory(origin, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(int id, int updateId, CancellationToken cancellationToken = default)
        {
            var ticket = _dbContext.Find<Ticket>(id);
            ticket.IsDeleted = true;
            ticket.UpdateUserId = updateId;
            ticket.UpdateTime = DateTime.Now;

            await CreateHistory(ticket, cancellationToken);

            _dbContext.Tickets.Update(ticket);

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task CreateHistory(Ticket origin, CancellationToken cancellationToken = default)
        {
            var ticketHistory = new TicketHistory
            {
                AsigneeUserId = origin.AsigneeUserId,
                CreationTime = origin.UpdateTime.Value,
                CreatorId = origin.UpdateUserId.Value,
                Description = origin.Description,
                Priority = origin.Priority,
                Severity = origin.Severity,
                Status = origin.Status,
                Summary = origin.Summary,
                Type = origin.Type,
                IsDeleted = origin.IsDeleted
            };

            await _dbContext.TicketHistories.AddAsync(ticketHistory, cancellationToken);
        }
    }
}