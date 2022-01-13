using System;

namespace TicketSystem.Infrastructure
{
    [Flags]
    public enum Role
    {
        Admin = 1,
        Pm = 2,
        Rd = 4,
        Qa = 8,
        All = Admin | Pm | Rd | Qa,
    }
}