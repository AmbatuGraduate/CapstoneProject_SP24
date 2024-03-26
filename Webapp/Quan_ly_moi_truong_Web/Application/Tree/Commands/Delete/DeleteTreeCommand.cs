using Application.Tree.Common;
using ErrorOr;
using MediatR;

namespace Application.Tree.Commands.Delete
{
    public record DeleteTreeCommand(string TreeCode) : IRequest<ErrorOr<AddTreeResult>>;
}