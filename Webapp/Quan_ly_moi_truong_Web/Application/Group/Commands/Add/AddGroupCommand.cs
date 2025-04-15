using Application.Group.Common;
using Application.Group.Common.Add_Update;
using ErrorOr;
using MediatR;

namespace Application.Group.Commands.Add
{
    public record AddGroupCommand(string accessToken, AddGoogleGroup group) : IRequest<ErrorOr<GroupResult>>;
}