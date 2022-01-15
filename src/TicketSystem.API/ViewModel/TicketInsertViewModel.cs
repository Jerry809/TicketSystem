using System.ComponentModel.DataAnnotations;
using TicketSystem.Infrastructure;

namespace TicketSystem.API.ViewModel
{
    public class TicketInsertViewModel
    {
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
        /// ticket 類型 (1:issue, 2:feature, 3:testCase)
        /// </summary>
        [Required]
        public TicketType Type { get; set; }

        /// <summary>
        /// 嚴重度 (1:low, 2:normal, 3:high
        /// </summary>
        public TicketSeverity? Severity { get; set; }

        /// <summary>
        /// 優先度 (1:low, 2:high)
        /// </summary>
        public Priority? Priority { get; set; }

        /// <summary>
        /// 受理人
        /// </summary>
        public int? AsigneeUserId { get; set; }
    }
}