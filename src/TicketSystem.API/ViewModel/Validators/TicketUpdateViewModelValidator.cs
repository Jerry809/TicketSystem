using FluentValidation;

namespace TicketSystem.API.ViewModel.Validators
{
    public class TicketUpdateViewModelValidator : AbstractValidator<TicketUpdateViewModel>
    {
        public TicketUpdateViewModelValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is empty");
            RuleFor(x => x.Summary).NotEmpty().WithMessage("Summary is empty");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is empty");
        }
    }
}