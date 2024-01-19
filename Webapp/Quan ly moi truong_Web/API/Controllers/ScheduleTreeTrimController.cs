using Application.ScheduleTreeTrim.Common;
using MapsterMapper;
using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Application.ScheduleTreeTrim.Queries.List;
using Contract.ScheduleTreeTrim;
using Application.ScheduleTreeTrim.Queries.GetById;
using Application.User.Queries.ListBySchedule;
using Application.ScheduleTreeTrim.Queries.GetStreets;
using Application.Street.Common;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    [EnableCors("AllowAllHeaders")]
    public class ScheduleTreeTrimController : ApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        // constructor
        public ScheduleTreeTrimController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        // get all schedule tree trims
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ErrorOr<List<ScheduleTreeTrimResult>> list = await mediator.Send(new ListScheduleTreeTrimQuery());

            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            List<ListScheduleTreeTrimResponse> scheduleTreeTrims = new List<ListScheduleTreeTrimResponse>();
            foreach (var scheduleTreeTrim in list.Value)
            {
                scheduleTreeTrims.Add(mapper.Map<ListScheduleTreeTrimResponse>(scheduleTreeTrim));
            }

            return Ok(scheduleTreeTrims);
        }

        // get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            ErrorOr<ScheduleTreeTrimResult> scheduleTreeTrim = await mediator.Send(new GetByIdQuery(id));

            if (scheduleTreeTrim.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: scheduleTreeTrim.FirstError.Description);
            }

            return Ok(mapper.Map<ListScheduleTreeTrimResponse>(scheduleTreeTrim.Value));
        }

        // get streets of schedule tree trim by schedule id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStreetsOfSchedule(string id)
        {
            var query = new GetStreetsQuery(Guid.Parse(id));
            ErrorOr<List<StreetResult>> result = await mediator.Send(query);

            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
            }

            return Ok(result.Value);
        }
    }
}
