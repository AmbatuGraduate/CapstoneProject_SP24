using FluentValidation;

namespace Application.GoogleAuthentication.Queries.GoogleRefresh
{
    public class GoogleRefreshValidator : AbstractValidator<GoogleRefreshQuery>
    {
        public GoogleRefreshValidator()
        {
        }
    }
}