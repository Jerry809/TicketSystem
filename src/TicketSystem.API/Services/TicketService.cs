using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketSystem.API.Services.Interfaces;
using TicketSystem.API.Services.Models;
using TicketSystem.Infrastructure;
using TicketSystem.Repository.Models.Filters;
using TicketSystem.Repository.Repositories.Interfaces;

namespace TicketSystem.API.Services
{
    public class TicketService : ITicketService
    {
        private readonly ILogger<TicketService> _logger;
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ILogger<TicketService> logger, ITicketRepository ticketRepository)
        {
            _logger = logger;
            _ticketRepository = ticketRepository;
        }

        public async Task<int> CreateTicketAsync(Ticket ticket, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _ticketRepository.CreateAsync(new Repository.Models.Ticket
                {
                    AsigneeUserId = ticket.AsigneeUserId,
                    Description = ticket.Description,
                    Priority = ticket.Priority.HasValue ? (int)ticket.Priority : null,
                    Severity = ticket.Severity.HasValue ? (int)ticket.Severity : null,
                    Summary = ticket.Summary,
                    Type = (int)ticket.Type,
                    Status = (int)ticket.Status,
                    CreatorId = ticket.CreatorId,
                    CreationTime = DateTime.Now,
                }, cancellationToken);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task UpdateTicketAsync(Ticket ticket, CancellationToken cancellationToken = default)
        {
            try
            {
                await _ticketRepository.UpdateAsync(new Repository.Models.Ticket
                {
                    Id = ticket.Id,
                    AsigneeUserId = ticket.AsigneeUserId,
                    Description = ticket.Description,
                    Priority = ticket.Priority,
                    Severity = ticket.Severity,
                    Summary = ticket.Summary,
                    Status = (int)ticket.Status,
                    UpdateUserId = ticket.CreatorId,
                    UpdateTime = DateTime.Now,
                }, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<Ticket> GetTicketAsync(int ticketId, CancellationToken cancellationToken = default)
        {
            var ticket = await _ticketRepository.GetTicketAsync(ticketId, cancellationToken);

            if (ticket == null)
                return null;

            return new Ticket()
            {
                Id = ticket.Id,
                Description = ticket.Description,
                Priority = ticket.Priority,
                Severity = ticket.Severity,
                Summary = ticket.Summary,
                Status = ticket.Status,
                Type = ticket.Type,
                AsigneeUserId = ticket.AsigneeUserId,
                CreatorId = ticket.CreatorId,
                CreationTime = ticket.CreationTime,
                UpdateTime = ticket.UpdateTime,
                UpdateId = ticket.UpdateUserId,
                Comments = ticket.Comments.Select(s => new Comment
                {
                    CreatorId = s.CreatorId,
                    Description = s.Description,
                    Id = s.Id,
                    TicketId = s.TicketId,
                    CreationTime = s.CreationTime,
                })
            };
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync(TicketFilter filter, CancellationToken cancellationToken = default)
        {
            var tickets = (await _ticketRepository.GetTicketListAsync(new GetTicketListFilter
            {
                Id = filter.Id,
                TicketType = filter.TicketType,
                AsigneeUserId = filter.AsigneeUserId,
                OrderBy = filter.OrderBy,
                OrderType = filter.OrderType,
            }, cancellationToken)).ToList();

            if (!tickets.Any())
                return new List<Ticket>();

            return tickets.Select(ticket => new Ticket()
            {
                Id = ticket.Id,
                Description = ticket.Description,
                Priority = ticket.Priority,
                Severity = ticket.Severity,
                Summary = ticket.Summary,
                Status = ticket.Status,
                Type = ticket.Type,
                AsigneeUserId = ticket.AsigneeUserId,
                CreatorId = ticket.CreatorId,
                CreationTime = ticket.CreationTime,
                UpdateTime = ticket.UpdateTime,
                UpdateId = ticket.UpdateUserId,
            });
        }

        public async Task<bool> ModifyTicketStatusOrAsigneeAsync(int id, int? status, int? asignee, int updateUserId,
            CancellationToken cancellationToken = default)

        {
            if (status.HasValue && asignee.HasValue)
            {
                await _ticketRepository.UpdateStatusAndAsigneeAsync(id, status.Value, asignee.Value, updateUserId, cancellationToken);

                return true;
            }

            if (status.HasValue)
            {
                await _ticketRepository.UpdateStatusAsync(id, status.Value, updateUserId, cancellationToken);

                return true;
            }

            await _ticketRepository.UpdateAsigneeAsync(id, asignee.Value, updateUserId, cancellationToken);

            return true;
        }

        public async Task<bool> DeleteTicketAsync(int id, int updateId, CancellationToken cancellationToken = default)
        {
            var result = await _ticketRepository.DeleteAsync(id, updateId, cancellationToken);

            return true;
        }
    }
}