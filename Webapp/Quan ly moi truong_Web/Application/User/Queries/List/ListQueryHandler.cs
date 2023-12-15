using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
using Application.Common.Interfaces.Persistence;
using Application.User.Common;
using Domain.Entities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.List
{
    public class ListQueryHandler
        : IRequestHandler<ListQuery, ErrorOr<List<UserResult>>>
    {
        private readonly IUserRepository userRepository;

        public ListQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ErrorOr<List<UserResult>>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            List<UserResult> userResults = new List<UserResult>();
            var list = userRepository.GetAll();

            foreach(var ls in list)
            {
                userResults.Add(new UserResult(ls));
            }

            return userResults;
        }
    }
}
