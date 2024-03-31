using Application.Tree.Common;
using ErrorOr;
using MediatR;

namespace Application.Tree.Commands.AutoUpdate
{
    public record AutoUpdateTreeCommand() : IRequest<ErrorOr<List<AddTreeResult>>>;
}