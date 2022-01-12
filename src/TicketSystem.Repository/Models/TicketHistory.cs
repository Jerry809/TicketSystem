using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TicketSystem.Repository.Models
{
    [Keyless]
    [Table("TicketHistory")]
    public partial class TicketHistory
    {
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Summary { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        public int Type { get; set; }
        public int? Severity { get; set; }
        public int? Priority { get; set; }
        public int? AsigneeUserId { get; set; }
        public int Status { get; set; }
        public int CreatorId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreationTime { get; set; }
    }
}
