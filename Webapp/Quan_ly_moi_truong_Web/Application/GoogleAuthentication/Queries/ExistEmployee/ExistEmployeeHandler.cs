using Application.Common.Interfaces.Authentication;
using ErrorOr;
using MediatR;

namespace Application.GoogleAuthentication.Queries.ExistEmployee
{
    public class ExistEmployeeHandler : IRequestHandler<ExistEmployeeQuery, ErrorOr<bool>>
    {
        private readonly IAuthenticationService authenticationService;

        public ExistEmployeeHandler(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        public async Task<ErrorOr<bool>> Handle(ExistEmployeeQuery request, CancellationToken cancellationToken)
        {
            return await authenticationService.EmployeeInOrganization(request.Email);
        }
    }
}
