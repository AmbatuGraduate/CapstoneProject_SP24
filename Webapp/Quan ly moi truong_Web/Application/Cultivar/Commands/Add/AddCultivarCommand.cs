using Application.Cultivar.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cultivar.Commands.Add
{
    public record AddCultivarCommand(
        string CultivarName,
        Guid TreeTypeId,
        string CreateBy,
        string UpdateBy
    ) : IRequest<ErrorOr<CultivarResult>>;
}
