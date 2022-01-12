using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TicketSystem.Repository.Models
{
    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        public int CreatorId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreationTime { get; set; }
        public int TicketId { get; set; }

        [ForeignKey(nameof(Id))]
        [InverseProperty(nameof(Ticket.Comment))]
        public virtual Ticket IdNavigation { get; set; }
    }
}
