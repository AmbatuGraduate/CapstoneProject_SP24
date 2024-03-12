using Application.Common.Interfaces.Persistence;
using Application.User.Common;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.User.Queries.GetById
{
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, ErrorOr<UserResult>>
    {
        private readonly IUserRepository userRepository;

        public GetByIdHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<UserResult>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            //var user = userRepository.GetById(request.UserId);

            //if (user == null)
            //{
            //    return Errors.GetUserById.getUserFail;
            //}

            return new UserResult(new ());
        }
    }
}
