using System.ComponentModel.DataAnnotations;
using TicketSystem.Infrastructure;

namespace TicketSystem.API.ViewModel
{
    public class TicketUpdateViewModel
    {
        /// <summary>
        /// ticket id
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [Required]
        public string Summary { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// 嚴重度 (1:low, 2:normal, 3:high
        /// </summary>
        public TicketSeverity? Severity { get; set; }

        /// <summary>
        /// 優先度 (1:low, 2:high)
        /// </summary>
        public Priority? Priority { get; set; }

        /// <summary>
        /// 受理人Id
        /// </summary>
        public int? AsigneeUserId { get; set; }
    }
}