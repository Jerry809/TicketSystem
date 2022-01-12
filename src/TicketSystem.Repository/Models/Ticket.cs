﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TicketSystem.Repository.Models
{
    [Table("Ticket")]
    public partial class Ticket
    {
        [Key]
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
        public int? UpdateUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; }

        [InverseProperty("IdNavigation")]
        public virtual Comment Comment { get; set; }
    }
}
