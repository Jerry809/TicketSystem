using FluentValidation;

namespace TicketSystem.API.ViewModel.Validators
{
    public class TicketInsertViewModelValidator : AbstractValidator<TicketInsertViewModel>
    {
        public TicketInsertViewModelValidator()
        {
            RuleFor(x => x.Summary).NotEmpty().WithMessage("Summary is empty");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is empty");
            RuleFor(x => x.Type).NotNull().IsInEnum().WithMessage("Type is not selected");
        }
    }
}