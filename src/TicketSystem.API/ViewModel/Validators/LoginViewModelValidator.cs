using FluentValidation;

namespace TicketSystem.API.ViewModel.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Account).NotEmpty().WithMessage("Account is empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password id empty");
        }
    }
}