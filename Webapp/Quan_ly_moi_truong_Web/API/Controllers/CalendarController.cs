using Application.Calendar;
using Application.Calendar.TreeCalendar.Queries.List;
using Application.Common.Interfaces.Authentication;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using Application.Calendar.TreeCalendar.Commands.Add;
using Application.Calendar.TreeCalendar.Commands.Update;
using Application.Calendar.TreeCalendar.Commands.Delete;
using Application.Calendar.TreeCalendar.Queries.GetByAttendeeId;
using Domain.Enums;
using Application.Calendar.TreeCalendar.Commands.UpdateJobStatus;
using Application.Calendar.TreeCalendar.Commands.AutoAdd;
using GoogleApi.Entities.Search.Common;
using System.Net;
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    [EnableCors("AllowAllHeaders")]
    public class CalendarController : ApiController
    {
        private readonly IMediator mediator;
        //private readonly IMapper mapper;

        //private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // constructor
        public CalendarController(IMediator mediator, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            //this.mapper = mapper;
            //_httpClient = httpClientFactory.CreateClient();
            _httpContextAccessor = httpContextAccessor;
        }

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

            return Ok(list.Value);
        }

        [HttpGet()]
        public async Task<IActionResult> GetCalendarEventsByAttendeeEmail(string token, string attendeeEmail)
        {
            System.Diagnostics.Debug.WriteLine("token: " + token);
            ErrorOr<List<MyEventResult>> list = await mediator.Send(new GetByAttendeeEmailQuery(token, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", attendeeEmail));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list.Value);
        }


        [HttpPost()]
        public async Task<IActionResult> AddCalendarEvent(string token, MyAddedEvent? myEvent)
        {
            ErrorOr<MyAddedEventResult> list = await mediator.Send(new AddCalendarCommand(token, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", myEvent));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list.Value);
        }

        [HttpGet]
        public async Task<IActionResult> AutoAddCalendarEvent()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            //Access HttpContext
            var token = httpContext.Request.Cookies["u_tkn"];
            System.Diagnostics.Debug.WriteLine("Checking: " + token);

            ErrorOr<List<MyAddedEventResult>>  list = await mediator.Send(new AutoAddTreeCalendarCommand(token, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com"));

            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list.Value);
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

        [HttpPost()]
        public async Task<IActionResult> UpdateJobWorkingStatus(string token, JobWorkingStatus jobWorkingStatus, string eventId)
        {
            ErrorOr<MyUpdatedJobStatusResult> list = await mediator.Send(new UpdateJobStatusCommand(token, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", jobWorkingStatus, eventId));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteCalendarEvent(string token, string eventId)
        {
            ErrorOr<MyDeletedEventResult> list = await mediator.Send(new DeleteCalendarCommand(token, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", eventId));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }
    }
}
