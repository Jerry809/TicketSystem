using System;
using System.Collections.Generic;
using TicketSystem.Infrastructure;

namespace TicketSystem.API.ViewModel
{
    public class TicketViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ticket 類型 (1:issue, 2:feature, 3:testCase)
        /// </summary>
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
        /// ticket 狀態 (1:new 2:active, 3:resolved, 4:closed) 
        /// </summary>
        public TicketStatus Status { get; set; }

        /// <summary>
        /// 受理人Id
        /// </summary>
        public int? AsigneeUserId { get; set; }

        /// <summary>
        /// 建立人Id
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 更新人Id
        /// </summary>
        public int? UpdateId { get; set; }

        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}