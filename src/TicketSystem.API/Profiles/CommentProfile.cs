using AutoMapper;
using TicketSystem.API.Services.Models;
using TicketSystem.API.ViewModel;

namespace TicketSystem.API.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentInsertViewModel, Comment>();
        }
    }
}