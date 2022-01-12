using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketSystem.API.Services.Models;

namespace TicketSystem.API.Services.Interfaces
{
    public interface ITicketService
    {
        Task CreateTicketAsync(Ticket ticket, CancellationToken cancellationToken = default);
        Task UpdateTicketAsync(Ticket ticket, CancellationToken cancellationToken = default);
        Task<Ticket> GetTicketAsync(int ticketId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Ticket>> GetTicketsAsync(TicketFilter filter, CancellationToken cancellationToken = default);
    }
}