using Microsoft.AspNetCore.Authorization;
using TicketSystem.Infrastructure;

namespace TicketSystem.API.Filters
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Role[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}