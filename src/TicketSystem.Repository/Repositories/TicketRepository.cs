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

        public async Task<int> Create(Ticket ticket, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(ticket, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return ticket.Id;
        }

        public async Task<int> Update(Ticket ticket, CancellationToken cancellationToken = default)
        {
            var origin = _dbContext.Find<Ticket>(ticket.Id);

            origin.Summary = ticket.Summary;
            origin.Description = ticket.Summary;
            origin.Priority = ticket.Priority;
            origin.Severity = ticket.Severity;
            origin.Status = ticket.Status;
            origin.AsigneeUserId = ticket.AsigneeUserId;
            origin.UpdateTime = DateTime.Now;
            origin.UpdateUserId = ticket.UpdateUserId;

            _dbContext.Update(origin);

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Ticket>> GetTicketList(GetTicketListFilter filter, CancellationToken cancellationToken = default)
        {
            Expression<Func<Ticket, bool>> predicate = x => true;

            if (filter.Id > 0)
            {
                predicate = predicate.And(x => x.Id == filter.Id);
            }

            if (filter.TicketType > 0)
            {
                predicate = predicate.And(x => x.Type == filter.TicketType);
            }

            var ticketQueryable = _dbContext.Tickets.Where(predicate).OrderByDescending(x => 0);

            var orderByColumn = typeof(Ticket).GetProperties()
                .FirstOrDefault(x => string.Equals(x.Name, filter.OrderBy, StringComparison.CurrentCultureIgnoreCase))?
                .Name;

            if (!string.IsNullOrWhiteSpace(orderByColumn))
            {
                ticketQueryable = filter.OrderType switch
                {
                    OrderType.AESC => ticketQueryable.ThenBy(orderByColumn),
                    OrderType.DESC => ticketQueryable.ThenByDescending(orderByColumn),
                    _ => ticketQueryable
                };
            }

            return await ticketQueryable.ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Ticket> GetTicket(int ticketId, CancellationToken cancellationToken = default)
        {
            var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(x => x.Id == ticketId, cancellationToken);

            return ticket;
        }
    }
}