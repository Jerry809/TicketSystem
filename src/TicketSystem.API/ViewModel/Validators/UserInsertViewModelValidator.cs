using FluentValidation;

namespace TicketSystem.API.ViewModel.Validators
{
    public class UserInsertViewModelValidator : AbstractValidator<UserInsertViewModel>
    {
        public UserInsertViewModelValidator()
        {
            RuleFor(x => x.Account).NotEmpty().WithMessage("Account is empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is empty");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is empty");
            RuleFor(x => x.Role).NotNull().IsInEnum().WithMessage("Role is not selected");
        }
    }
}