using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketSystem.Repository.Models;
using TicketSystem.Repository.Models.Filters;

namespace TicketSystem.Repository.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<int> CreateAsync(Ticket ticket, CancellationToken cancellationToken = default);

        Task<int> UpdateAsync(Ticket ticket, CancellationToken cancellationToken = default);

        Task<IEnumerable<Ticket>> GetTicketListAsync(GetTicketListFilter filter, CancellationToken cancellationToken = default);

        Task<Ticket> GetTicketAsync(int ticketId, CancellationToken cancellationToken = default);
    }
}