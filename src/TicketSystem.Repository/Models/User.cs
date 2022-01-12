using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TicketSystem.Repository.Models
{
    [Table("User")]
    public partial class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public int Role { get; set; }
        public int CreatorId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreationTime { get; set; }
        public int? UpdateUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; }
        public bool IsDisabled { get; set; }
    }
}
