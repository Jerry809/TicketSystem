using System;

namespace TicketSystem.API.Services.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public int TicketId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}