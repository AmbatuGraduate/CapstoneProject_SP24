using Application.Cultivar.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cultivar.Queries.GetById
{ 
    public record GetByIdQuery(Guid CultivarId) : IRequest<ErrorOr<CultivarResult>>;

}
