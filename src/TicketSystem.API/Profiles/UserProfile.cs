using AutoMapper;
using TicketSystem.API.Services.Models;
using TicketSystem.API.ViewModel;

namespace TicketSystem.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserInsertViewModel, User>();
        }
    }
}