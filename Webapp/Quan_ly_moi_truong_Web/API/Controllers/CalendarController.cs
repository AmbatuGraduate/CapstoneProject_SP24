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
        [HttpGet()]
        public async Task<IActionResult> GetAllCalendarEvents()
        {
            var clientType = Request.Headers["Client-Type"];


            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }

            // ========================
            ErrorOr<List<MyEvent>> list = await mediator.Send(new ListTreeCalendarQuery(accessToken, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com"));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list.Value);
        }

        [HttpGet()]
        public async Task<IActionResult> GetCalendarEventsByAttendeeEmail(string attendeeEmail)
        {
            var clientType = Request.Headers["Client-Type"];


            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }
            ErrorOr<List<MyEventResult>> list = await mediator.Send(new GetByAttendeeEmailQuery(accessToken, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", attendeeEmail));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list.Value);
        }


        [HttpPost()]
        public async Task<IActionResult> AddCalendarEvent(MyAddedEvent? myEvent)
        {
            var clientType = Request.Headers["Client-Type"];


            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }
            ErrorOr<MyAddedEventResult> list = await mediator.Send(new AddCalendarCommand(accessToken, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", myEvent));
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
        public async Task<IActionResult> UpdateCalendarEvent(MyUpdatedEvent? myEvent, string eventId)
        {
            var clientType = Request.Headers["Client-Type"];


            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }
            ErrorOr<MyUpdatedEventResult> list = await mediator.Send(new UpdateCalendarCommand(accessToken, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", myEvent, eventId));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }

        [HttpPost()]
        public async Task<IActionResult> UpdateJobWorkingStatus([FromBody] UpdateJobStatusCommand command)
        {
            var clientType = Request.Headers["Client-Type"];


            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }

            ErrorOr<MyUpdatedJobStatusResult> list = await mediator.Send(new UpdateJobStatusCommand(accessToken, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", command.jobWorkingStatus, command.eventId));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteCalendarEvent(string eventId)
        {
            var clientType = Request.Headers["Client-Type"];


            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }
            ErrorOr<MyDeletedEventResult> list = await mediator.Send(new DeleteCalendarCommand(accessToken, "c_6529bcce12126756f2aa18387c15b6c1fee86014947d41d8a5b9f5d4170c4c4a@group.calendar.google.com", eventId));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }
    }
}
