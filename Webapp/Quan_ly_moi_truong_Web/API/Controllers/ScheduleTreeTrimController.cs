using Application.Calendar;
using Application.Calendar.TreeCalendar.Queries.List;
using Application.Common.Interfaces.Authentication;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using Application.Calendar;
using Application.Calendar.TreeCalendar.Queries.List;
using Application.Calendar.TreeCalendar.Commands.Add;
using Application.Calendar.TreeCalendar.Commands.Update;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    [EnableCors("AllowAllHeaders")]
    public class ScheduleTreeTrimController : ApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        private readonly HttpClient _httpClient;
        private readonly ISessionService _sessionService;


        // constructor
        public ScheduleTreeTrimController(IMediator mediator, IMapper mapper, IHttpClientFactory httpClientFactory, ISessionService sessionService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            _httpClient = httpClientFactory.CreateClient();
            _sessionService = sessionService;
        }

        // get all schedule tree trims
        // [HttpGet]
        // public async Task<IActionResult> Get()
        // {
        //     ErrorOr<List<ScheduleTreeTrimResult>> list = await mediator.Send(new ListScheduleTreeTrimQuery());

        //     if (list.IsError)
        //     {
        //         return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
        //     }

        //     List<ListScheduleTreeTrimResponse> scheduleTreeTrims = new List<ListScheduleTreeTrimResponse>();
        //     foreach (var scheduleTreeTrim in list.Value)
        //     {
        //         scheduleTreeTrims.Add(mapper.Map<ListScheduleTreeTrimResponse>(scheduleTreeTrim));
        //     }

        //     return Ok(scheduleTreeTrims);
        // }

        // // get by id
        // [HttpGet("{id}")]
        // public async Task<IActionResult> Get(Guid id)
        // {
        //     ErrorOr<ScheduleTreeTrimResult> scheduleTreeTrim = await mediator.Send(new GetByIdQuery(id));

        //     if (scheduleTreeTrim.IsError)
        //     {
        //         return Problem(statusCode: StatusCodes.Status400BadRequest, title: scheduleTreeTrim.FirstError.Description);
        //     }

        //     return Ok(mapper.Map<ListScheduleTreeTrimResponse>(scheduleTreeTrim.Value));
        // }

        // // get streets of schedule tree trim by schedule id
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetStreetsOfSchedule(string id)
        // {
        //     var query = new GetStreetsQuery(Guid.Parse(id));
        //     ErrorOr<List<StreetResult>> result = await mediator.Send(query);

        //     if (result.IsError)
        //     {
        //         return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
        //     }

        //     return Ok(result.Value);
        // }

        // get google calendar events
        [HttpGet("{token}")]
        public async Task<IActionResult> GetCalendarEvents(string token)
        {
            System.Diagnostics.Debug.WriteLine("token: " + token);
            ErrorOr<List<MyEventResult>> list = await mediator.Send(new ListTreeCalendarQuery(token, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com"));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }

        [HttpPost()]
        public async Task<IActionResult> AddCalendarEvent(string token, MyAddedEvent? myEvent)
        {
            ErrorOr<MyAddedEventResult> list = await mediator.Send(new AddCalendarCommand(token, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", myEvent));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }

        [HttpPost()]
        public async Task<IActionResult> UpdateCalendarEvent(string token, MyUpdatedEvent? myEvent, string eventId)
        {
            ErrorOr<MyUpdatedEventResult> list = await mediator.Send(new UpdateCalendarCommand(token, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", myEvent, eventId));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }
    }
}
