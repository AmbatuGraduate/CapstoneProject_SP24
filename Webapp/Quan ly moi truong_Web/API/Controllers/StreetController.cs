
using Application.Street.Common;
using Application.Street.Queries.GetById;
using Application.Street.Queries.List;
using Application.Tree.Common;
using Application.Tree.Queries.List;
using Contract.Street;
using Contract.Tree;
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

            var result = await mediator.Send(query);

            return Ok(result);
        }
    }
}
