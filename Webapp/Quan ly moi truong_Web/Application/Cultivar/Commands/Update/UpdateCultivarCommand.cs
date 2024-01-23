using Application.Cultivar.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cultivar.Commands.Update
{
    public record UpdateCultivarCommand(
        Guid CultivarId,
        string CultivarName,
        Guid TreeTypeId,
        string UpdateBy
    ) : IRequest<ErrorOr<CultivarResult>>;
}
