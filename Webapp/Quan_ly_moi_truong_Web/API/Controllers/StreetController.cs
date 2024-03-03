using Application.Street.Commands.Add;
using Application.Street.Commands.Delete;
using Application.Street.Commands.Update;
using Application.Street.Common;
using Application.Street.Queries.GetById;
using Application.Street.Queries.List;
using Contract.Street;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    [EnableCors("AllowAllHeaders")]
    public class StreetController : ApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public StreetController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        // Get all street 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            /*var list = await mediator.Send(new ListStreetQuery());

            return Ok(list);*/

            ErrorOr<List<StreetResult>> list = await mediator.Send(new ListStreetQuery());

            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            List<ListStreetResponse> streets = new List<ListStreetResponse>();
            foreach (var street in list.Value)
            {
                streets.Add(mapper.Map<ListStreetResponse>(street));
            }

            return Ok(streets);
        }

        // get street by id
        [HttpGet("{StreetId}")]
        public async Task<IActionResult> GetById(string StreetId)
        {
            var query = mapper.Map<GetByIdQuery>(Guid.Parse(StreetId));

            ErrorOr<StreetResult> result = await mediator.Send(query);

            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
            }

            return Ok(mapper.Map<DetailsStreetResponse>(result.Value));
        }

        // Add Street
        [HttpPost]
        public async Task<IActionResult> Add(AddStreetRequest request)
        {
            var command = mapper.Map<AddStreetCommand>(request);

            ErrorOr<StreetResult> addResult = await mediator.Send(command);

            return addResult.Match(
                streetToAdd => Ok(mapper.Map<DetailsStreetResponse>(addResult)),
                errors => Problem(errors)
                );
        }

        // Delete Street
        [HttpDelete("{StreetId}")]
        public async Task<IActionResult> Delete(string StreetId)
        {
            var command = mapper.Map<DeleteStreetCommand>(Guid.Parse(StreetId));

            ErrorOr<StreetResult> deleteResult = await mediator.Send(command);

            return deleteResult.Match(
                               streetToDelete => Ok(mapper.Map<DetailsStreetResponse>(deleteResult)),
                               errors => Problem(errors)
            );
        }

        // Update Street
        [HttpPut("{StreetId}")]
        public async Task<IActionResult> Update(string StreetId, UpdateStreetRequest request)
        {
            var command = mapper.Map<UpdateStreetCommand>((StreetId, request));

            ErrorOr<StreetResult> updateResult = await mediator.Send(command);

            if (updateResult.IsError && updateResult.FirstError == Domain.Common.Errors.Errors.GetStreetById.getStreetFail)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: updateResult.FirstError.Description);
            }

            return updateResult.Match(
                updateResult => Ok(mapper.Map<DetailsStreetResponse>(updateResult)),
                errors => Problem(errors));
        }
    }
}
