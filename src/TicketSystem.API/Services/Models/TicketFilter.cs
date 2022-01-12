using TicketSystem.Infrastructure;

namespace TicketSystem.API.Services.Models
{
    public class TicketFilter
    {
        public int? Id { get; set; }
        public int? TicketType { get; set; }
        public int? AsigneeUserId { get; set; }
        public string OrderBy { get; set; }
        public OrderType? OrderType { get; set; }
    }
}