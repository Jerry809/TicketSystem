using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TicketSystem.Repository.Models
{
    [Keyless]
    [Table("Comment")]
    public partial class Comment
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        public int CreatorId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreationTime { get; set; }
    }
}
