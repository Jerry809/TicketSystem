using System.ComponentModel.DataAnnotations;
using TicketSystem.Infrastructure;

namespace TicketSystem.API.ViewModel
{
    public class UserInsertViewModel
    {
        /// <summary>
        /// 帳號
        /// </summary>
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [Required]
        public Role Role { get; set; }
    }
}