using Application.Cultivar.Commands.Add;
using Application.Cultivar.Commands.Update;
using Application.Cultivar.Common;
using Application.Cultivar.Queries.GetById;
using Application.Cultivar.Queries.List;
using Contract.Cultivar;
using Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    [EnableCors("AllowAllHeaders")]

    public class CultivarController : ApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CultivarController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ErrorOr<List<CultivarResult>> list = await mediator.Send(new ListCultivarQuery());

            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            List<ListCultivarRepsone> cultivars = new List<ListCultivarRepsone>();
            foreach (var cultivar in list.Value)
            {
                cultivars.Add(mapper.Map<ListCultivarRepsone>(cultivar));
            }

            return Ok(cultivars);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            var query = mapper.Map<GetByIdQuery>(Guid.Parse(Id));

            ErrorOr<CultivarResult> result = await mediator.Send(query);

            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
            }

            return Ok(mapper.Map<ListCultivarRepsone>(result.Value));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCultivarRequest request)
        {
            var command = mapper.Map<AddCultivarCommand>(request);

            ErrorOr<CultivarResult> addResult = await mediator.Send(command);

            return addResult.Match(
                treeToAdd => Ok(mapper.Map<ListCultivarRepsone>(addResult)),
                errors => Problem(errors)
                );
        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(string Id, UpdateCultivarRequest request)
        {
            var command = mapper.Map<UpdateCultivarCommand>((Guid.Parse(Id), request));

            ErrorOr<CultivarResult> updateResult = await mediator.Send(command);

            if (updateResult.IsError && updateResult.FirstError == Errors.GetCultivarById.getCultivarById)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: updateResult.FirstError.Description);
            }

            return updateResult.Match(
                updateResult => Ok(mapper.Map<ListCultivarRepsone>(updateResult)),
                errors => Problem(errors));
        }

    }
}
