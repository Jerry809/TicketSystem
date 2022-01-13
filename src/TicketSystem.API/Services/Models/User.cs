using TicketSystem.Infrastructure;

namespace TicketSystem.API.Services.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
        public int CreatorId { get; set; }
        public bool IsDisabled { get; set; }
    }
}