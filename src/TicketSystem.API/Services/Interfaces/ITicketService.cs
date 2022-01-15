using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketSystem.API.Services.Models;

namespace TicketSystem.API.Services.Interfaces
{
    public interface ITicketService
    {
        Task<int> CreateTicketAsync(Ticket ticket, CancellationToken cancellationToken = default);
        Task UpdateTicketAsync(Ticket ticket, CancellationToken cancellationToken = default);
        Task<Ticket> GetTicketAsync(int ticketId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Ticket>> GetTicketsAsync(TicketFilter filter, CancellationToken cancellationToken = default);

        Task<bool> ModifyTicketStatusOrAsigneeAsync(int id, int? status, int? asignee, int updateUserId,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteTicketAsync(int id, int updateId, CancellationToken cancellationToken = default);
    }
}