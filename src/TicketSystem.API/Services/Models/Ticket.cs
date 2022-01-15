using System;
using System.Collections;
using System.Collections.Generic;
using TicketSystem.Infrastructure;

namespace TicketSystem.API.Services.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int? Severity { get; set; }
        public int? Priority { get; set; }
        public int? AsigneeUserId { get; set; }
        public int Status { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public int? UpdateId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}