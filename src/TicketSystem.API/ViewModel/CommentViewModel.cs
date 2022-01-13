using System;

namespace TicketSystem.API.ViewModel
{
    /// <summary>
    /// 評論
    /// </summary>
    public class CommentViewModel
    {
        public int Id { get; set; }
        
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// ticket單號
        /// </summary>
        public int TicketId { get; set; }
        
        /// <summary>
        /// 建檔人
        /// </summary>
        public int CreatorId { get; set; }
        
        /// <summary>
        /// 建檔時間
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}