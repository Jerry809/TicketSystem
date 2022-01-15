using System.ComponentModel.DataAnnotations;
using TicketSystem.Infrastructure;

namespace TicketSystem.API.ViewModel
{
    public class TicketStatusUpdateViewModel
    {
        /// <summary>
        /// ticket id
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 受理人
        /// </summary>
        public int? AsigneeUserId { get; set; }

        /// <summary>
        /// ticket 狀態 (1:new 2:active, 3:resolved, 4:closed)
        /// </summary>
        public TicketStatus? Status { get; set; }
    }
}