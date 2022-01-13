using FluentValidation;

namespace TicketSystem.API.ViewModel.Validators
{
    public class CommentInsertViewModelValidator : AbstractValidator<CommentInsertViewModel>
    {
        public CommentInsertViewModelValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("description is empty");
            RuleFor(x => x.TicketId).GreaterThan((0)).WithMessage("ticket id is zero");
        }
    }
}