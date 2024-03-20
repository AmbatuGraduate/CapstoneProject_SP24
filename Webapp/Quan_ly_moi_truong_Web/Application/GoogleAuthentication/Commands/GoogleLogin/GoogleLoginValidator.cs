using FluentValidation;

namespace Application.GoogleAuthentication.Commands.GoogleLogin
{
    public class GoogleLoginValidator : AbstractValidator<GoogleLoginCommand>
    {
        public GoogleLoginValidator()
        {
            RuleFor(x => x.authCode).NotEmpty();
        }
    }
}