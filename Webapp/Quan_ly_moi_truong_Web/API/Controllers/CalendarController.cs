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
using Application.GoogleAuthentication.Queries.GoogleAccessToken;
using Application.GoogleAuthentication.Common;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    [EnableCors("AllowAllHeaders")]
    public class CalendarController : ApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        private readonly HttpClient _httpClient;
        private readonly ISessionService _sessionService;


        // constructor
        public CalendarController(IMediator mediator, IMapper mapper, IHttpClientFactory httpClientFactory, ISessionService sessionService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            _httpClient = httpClientFactory.CreateClient();
            _sessionService = sessionService;
        }

        // get google calendar events
        [HttpGet()]
        public async Task<IActionResult> GetAllCalendarEvents()
        {
            var jwt = Request.Cookies["u_tkn"];
            System.Diagnostics.Debug.WriteLine("token: " + jwt);
            ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
            ErrorOr<List<MyEvent>> list = await mediator.Send(new ListTreeCalendarQuery(token.Value.accessToken, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com"));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list.Value);
        }

        [HttpGet()]
        public async Task<IActionResult> GetCalendarEventsByAttendeeEmail(string attendeeEmail)
        {
            var jwt = Request.Cookies["u_tkn"];
            System.Diagnostics.Debug.WriteLine("token: " + jwt);
            ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
            ErrorOr<List<MyEventResult>> list = await mediator.Send(new GetByAttendeeEmailQuery(token.Value.accessToken, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", attendeeEmail));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }

        [HttpPost()]
        public async Task<IActionResult> AddCalendarEvent(MyAddedEvent? myEvent)
        {
            var jwt = Request.Cookies["u_tkn"];
            System.Diagnostics.Debug.WriteLine("token: " + jwt);
            ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
            ErrorOr<MyAddedEventResult> list = await mediator.Send(new AddCalendarCommand(token.Value.accessToken, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", myEvent));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }

        [HttpPost()]
        public async Task<IActionResult> UpdateCalendarEvent(MyUpdatedEvent? myEvent, string eventId)
        {
            var jwt = Request.Cookies["u_tkn"];
            System.Diagnostics.Debug.WriteLine("token: " + jwt);
            ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
            ErrorOr<MyUpdatedEventResult> list = await mediator.Send(new UpdateCalendarCommand(token.Value.accessToken, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", myEvent, eventId));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }

        [HttpPost()]
        public async Task<IActionResult> UpdateJobWorkingStatus(JobWorkingStatus jobWorkingStatus, string eventId)
        {
            var jwt = Request.Cookies["u_tkn"];
            System.Diagnostics.Debug.WriteLine("token: " + jwt);
            ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
            ErrorOr<MyUpdatedJobStatusResult> list = await mediator.Send(new UpdateJobStatusCommand(token.Value.accessToken, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", jobWorkingStatus, eventId));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteCalendarEvent(string eventId)
        {
            var jwt = Request.Cookies["u_tkn"];
            System.Diagnostics.Debug.WriteLine("token: " + jwt);
            ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
            ErrorOr<MyDeletedEventResult> list = await mediator.Send(new DeleteCalendarCommand(token.Value.accessToken, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", eventId));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }
    }
}
