using Application.Group.Common;
using Application.Group.Common.Add_Update;
using ErrorOr;
using MediatR;

namespace Application.Group.Commands.Update
{
    public record UpdateGroupCommand(string accessToken, UpdateGoogleGroup group) : IRequest<ErrorOr<GroupResult>>;
}