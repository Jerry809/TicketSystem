using FluentValidation;

namespace TicketSystem.API.ViewModel.Validators
{
    public class TicketStatusUpdateViewModelValidator : AbstractValidator<TicketStatusUpdateViewModel>
    {
        public TicketStatusUpdateViewModelValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is empty");
        }
    }
}