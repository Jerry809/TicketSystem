using System.ComponentModel.DataAnnotations;

namespace TicketSystem.API.ViewModel
{
    /// <summary>
    /// 評論
    /// </summary>
    public class CommentInsertViewModel
    {
        /// <summary>
        /// 描述
        /// </summary>
        [Required]
        public string Description { get; set; }
        
        /// <summary>
        /// ticket單號
        /// </summary>
        [Required]
        public int TicketId { get; set; }
    }
}