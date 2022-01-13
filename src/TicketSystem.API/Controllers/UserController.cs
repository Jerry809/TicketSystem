using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IMapper _mapper;

        public UserController(
            ILogger<UserController> logger,
            IUserService userService,
            IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel request, CancellationToken cancellationToken = default)
        {
            var result = await _userService.LoginAsync(request.Account, request.Password, cancellationToken);

            if (!result.isOk)
                return BadRequest("Account or Password Incorrect");
            
            return Ok(result.jwt);
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserInsertViewModel request, CancellationToken cancellationToken = default)
        {
            var user = _mapper.Map<User>(request);
            int.TryParse(User?.Identity?.Name, out var userId);

            user.CreatorId = userId;

            var result = await _userService.CreateUserAsync(user, cancellationToken);

            if (result == -1)
                return BadRequest("account is exist");
            
            return Ok(result);
        }
    }
}