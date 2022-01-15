using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketSystem.API.Constants;
using TicketSystem.API.Filters;
using TicketSystem.API.Services.Interfaces;
using TicketSystem.API.Services.Models;
using TicketSystem.API.ViewModel;
using TicketSystem.Infrastructure;

namespace TicketSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(
            ILogger<UserController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel request, CancellationToken cancellationToken = default)
        {
            var result = await _userService.LoginAsync(request.Account, request.Password, cancellationToken);

            if (!result.isOk)
                return BadRequest(BadRequestConstants.AccountOrPasswordIncorrect);
            
            return Ok(result.jwt);
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserInsertViewModel request, CancellationToken cancellationToken = default)
        {
            int.TryParse(User?.Identity?.Name, out var userId);

            var result = await _userService.CreateUserAsync(new User
            {
                Account = request.Account,
                Password = request.Password,
                Role = (int)request.Role,
                Name = request.Name,
                CreatorId = userId,
                
            }, cancellationToken);

            if (result == -1)
                return BadRequest(BadRequestConstants.AccountIsExist);
            
            return Ok(result);
        }
    }
}