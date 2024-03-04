using FluentValidation;

namespace Application.GoogleAuthentication.Queries.GoogleLogin
{
    public class GoogleLoginValidator : AbstractValidator<GoogleLoginQuery>
    {
        public GoogleLoginValidator()
        {
            RuleFor(x => x.authCode).NotEmpty();
        }
    }
}