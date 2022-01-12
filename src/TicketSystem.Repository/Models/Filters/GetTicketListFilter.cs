using TicketSystem.Infrastructure;

namespace TicketSystem.Repository.Models.Filters
{
    public class GetTicketListFilter
    {
        public int Id { get; set; }
        public int TicketType { get; set; }
        public int AsigneeUserId { get; set; }
        public string OrderBy { get; set; }
        public OrderType OrderType { get; set; }
    }
}