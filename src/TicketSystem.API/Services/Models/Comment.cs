namespace TicketSystem.API.Services.Models
{
    public class Comment
    {
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public int TicketId { get; set; }
    }
}