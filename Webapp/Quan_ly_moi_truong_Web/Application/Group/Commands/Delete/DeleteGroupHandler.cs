using Application.Common.Interfaces.Persistence;
using Application.Group.Commands.Add;
using Application.Group.Common;
using Domain.Entities.Deparment;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Group.Commands.Delete
{
    public class DeleteGroupHandler : IRequestHandler<DeleteGroupCommand, ErrorOr<bool>>
    {
        private readonly IGroupRepository groupRepository;

        public DeleteGroupHandler(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }
        public async Task<ErrorOr<bool>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var result = false;
            await Task.CompletedTask;
            var addedGroup = await groupRepository.DeleteGoogleGroup(request.accessToken, request.groupEmail);

            result = groupRepository.DeleteGroupDB(request.groupEmail);

            return result;
        }
    }
}
