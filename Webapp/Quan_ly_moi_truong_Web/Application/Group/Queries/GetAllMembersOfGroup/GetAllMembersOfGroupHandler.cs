﻿using Application.Common.Interfaces.Persistence;
using Application.Group.Common;
using Application.Group.Queries.GetGroup;
using Application.User.Common.List;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Group.Queries.GetAllMembersOfGroup
{
    public class GetAllMembersOfGroupHandler : IRequestHandler<GetAllMembersOfGroupQuery, ErrorOr<List<GoogleUser>>>
    {
        private readonly IGroupRepository groupRepository;

        public GetAllMembersOfGroupHandler(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }
        public async Task<ErrorOr<List<GoogleUser>>> Handle(GetAllMembersOfGroupQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var usersResult = await groupRepository.GetAllMembersOfGroup(request.accessToken, request.groupEmail);
            return usersResult;
        }
    }
}
