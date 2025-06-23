namespace PeopleHub.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.LoginRequest.Username)
            .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.LoginRequest.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
