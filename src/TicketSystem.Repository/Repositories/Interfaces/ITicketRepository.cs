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
        Task<int> Create(Ticket ticket, CancellationToken cancellationToken = default);

        Task<int> Update(Ticket ticket, CancellationToken cancellationToken = default);

        Task<IEnumerable<Ticket>> GetTicketList(GetTicketListFilter filter, CancellationToken cancellationToken = default);

        Task<Ticket> GetTicket(int ticketId, CancellationToken cancellationToken = default);
    }
}