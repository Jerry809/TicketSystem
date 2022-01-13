using System.ComponentModel.DataAnnotations;

namespace TicketSystem.API.ViewModel
{
    public class LoginViewModel
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
    }
}