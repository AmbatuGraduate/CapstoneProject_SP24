using Application.Calendar;
using Application.Calendar.TreeCalendar.Commands.Add;
using Application.Calendar.TreeCalendar.Commands.AutoAdd;
using Application.Calendar.TreeCalendar.Commands.AutoUpdateJobStatus;
using Application.Calendar.TreeCalendar.Commands.Delete;
using Application.Calendar.TreeCalendar.Commands.Update;
using Application.Calendar.TreeCalendar.Commands.UpdateJobStatus;
using Application.Calendar.TreeCalendar.Queries.GetByAttendeeId;
using Application.Calendar.TreeCalendar.Queries.GetCalendarIdByCalendarType;
using Application.Calendar.TreeCalendar.Queries.GetEventById;
using Application.Calendar.TreeCalendar.Queries.List;
using Application.Calendar.TreeCalendar.Queries.ListCalendarNotHaveAttendees;
using Application.Calendar.TreeCalendar.Queries.ListCurrentDayEventsByEmail;
using Application.Calendar.TreeCalendar.Queries.ListLateCalendar;
using Application.Calendar.TreeCalendar.Queries.NumberOfEventsToday;
using Application.GoogleAuthentication.Common;
using Application.GoogleAuthentication.Queries.GoogleAccessToken;
using Domain.Enums;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Application.Calendar.TreeCalendar.Queries.GetCalendarByDepartmentEmail;
using Application.Calendar.TreeCalendar.Common;
using Infrastructure.Authentication.AuthenticationAttribute;

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

        //private readonly INotifyService notifyService;

        // constructor
        public CalendarController(IMediator mediator, IMapper mapper, IHttpContextAccessor httpContextAccessor /*INotifyService notifyService*/)
        {
            this.mediator = mediator;
            //this.mapper = mapper;
            //_httpClient = httpClientFactory.CreateClient();
            _httpContextAccessor = httpContextAccessor;
            //this.notifyService = notifyService;
        }

        // get google calendar events
        [HttpGet()]
        [Authorize(Roles = "Admin, Manager, Employee")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> GetAllCalendarEvents(CalendarTypeEnum calendarTypeEnum)
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
                var jwt = _httpContextAccessor.HttpContext.Request.Cookies["u_tkn"];
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

            var calendarId = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(calendarTypeEnum));

            // ========================
            ErrorOr<List<MyEvent>> list = await mediator.Send(new ListTreeCalendarQuery(accessToken, calendarId.Value));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list.Value);
        }

        // get google calendar events by attendee email
        [HttpGet()]
        [Authorize(Roles = "Admin, Manager, Employee")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> GetCalendarEventsByAttendeeEmail(CalendarTypeEnum calendarTypeEnum, string attendeeEmail)
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
            var calendarId = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(calendarTypeEnum));
            ErrorOr<List<MyEventResult>> list = await mediator.Send(new GetByAttendeeEmailQuery(accessToken, calendarId.Value, attendeeEmail));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list.Value);
        }

        // get google calendar events by department email
        [HttpGet()]
        [Authorize(Roles = "Admin, Manager, Employee")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> GetCalendarEventsByDepartmentEmail(CalendarTypeEnum calendarTypeEnum, string departmentEmail)
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
            var calendarId = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(calendarTypeEnum));
            ErrorOr<List<MyEventResult>> list = await mediator.Send(new GetCalendarByDepartmentEmailCommand(accessToken, calendarId.Value, departmentEmail));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list.Value);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> GetAllLateCalendarEvent(CalendarTypeEnum calendarTypeEnum)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            //Access HttpContext
            var token = httpContext.Request.Cookies["u_tkn"];
            System.Diagnostics.Debug.WriteLine("Checking: " + token);
            var calendarId = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(calendarTypeEnum));
            ErrorOr<List<MyEvent>> list = await mediator.Send(new ListLateCalendarQuery(token, calendarId.Value));

            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list.Value);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> GetAllCalendarEventNoAttendees(CalendarTypeEnum calendarTypeEnum)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            //Access HttpContext
            var token = httpContext.Request.Cookies["u_tkn"];
            System.Diagnostics.Debug.WriteLine("Checking: " + token);
            var calendarId = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(calendarTypeEnum));
            ErrorOr<List<MyEvent>> list = await mediator.Send(new ListCalendarNotHaveAttendessQuery(token, calendarId.Value));

            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list.Value);
        }

        // add events
        [HttpPost()]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> AddCalendarEvent(CalendarTypeEnum calendarTypeEnum, MyAddedEvent? myEvent)
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
            var calendarId = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(calendarTypeEnum));
            ErrorOr<MyAddedEventResult> list = await mediator.Send(new AddCalendarCommand(accessToken, calendarId.Value, myEvent));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list.Value);
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> AutoUpdateCalendarJobStatus(CalendarTypeEnum calendarTypeEnum)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            //Access HttpContext
            var token = httpContext.Request.Cookies["u_tkn"];
            System.Diagnostics.Debug.WriteLine("Checking: " + token);
            var calendarId = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(calendarTypeEnum));
            ErrorOr<List<MyUpdatedJobStatusResult>> list = await mediator.Send(new AutoUpdateJobStatusCommand(token, calendarId.Value));

            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            //Use signalR

            return Ok(list.Value);
        }

        // auto add events
        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> AutoAddCalendarEvent(CalendarTypeEnum calendarTypeEnum)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            //Access HttpContext
            var token = httpContext.Request.Cookies["u_tkn"];
            System.Diagnostics.Debug.WriteLine("Checking: " + token);
            var calendarId = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(calendarTypeEnum));
            ErrorOr<List<MyAddedEventResult>> list = await mediator.Send(new AutoAddTreeCalendarCommand(token, calendarId.Value));

            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list.Value);
        }

        // update events
        [HttpPost()]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> UpdateCalendarEvent(CalendarTypeEnum calendarTypeEnum, MyUpdatedEvent? myEvent, string eventId)
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
            var calendarId = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(calendarTypeEnum));
            ErrorOr<MyUpdatedEventResult> list = await mediator.Send(new UpdateCalendarCommand(accessToken, calendarId.Value, myEvent, eventId));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }

        // update job status
        [HttpPost()]
        public async Task<IActionResult> UpdateJobWorkingStatus(CalendarTypeEnum calendarTypeEnum, [FromBody] UpdateJobStatusRequest request)
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
            var calendarId = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(calendarTypeEnum));
            ErrorOr<MyUpdatedJobStatusResult> list = await mediator.Send(new UpdateJobStatusCommand(accessToken, calendarId.Value, request.jobWorkingStatus, request.eventId));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }

        // delete events
        [HttpDelete()]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> DeleteCalendarEvent(CalendarTypeEnum calendarTypeEnum, string eventId)
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
            var calendarId = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(calendarTypeEnum));
            ErrorOr<MyDeletedEventResult> list = await mediator.Send(new DeleteCalendarCommand(accessToken, calendarId.Value, eventId));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list);
        }

        // get current day events by attendee email
        [HttpGet()]
        public async Task<IActionResult> GetCurrentDayEventsByEmail(CalendarTypeEnum calendarTypeEnum, string attendeeEmail)
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
            var calendarId = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(calendarTypeEnum));
            ErrorOr<List<MyEventResult>> list = await mediator.Send(new ListCurrentEventsQuery(accessToken, calendarId.Value, attendeeEmail));
            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            return Ok(list.Value);
        }

        // get number of events by attendee email
        [HttpGet()]
        public async Task<IActionResult> NumberOfEventsUser(CalendarTypeEnum calendarTypeEnum, string attendeeEmail)
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
            var calendarId = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(calendarTypeEnum));
            ErrorOr<int> quantity = await mediator.Send(new NumberOfEventsQuery(accessToken, calendarId.Value, attendeeEmail));
            if (quantity.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: quantity.FirstError.Description);
            }

            return Ok(quantity.Value);
        }

        // get event by id
        [HttpGet()]
        public async Task<IActionResult> GetEventById(CalendarTypeEnum calendarTypeEnum, string eventId)
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

            var calendarId = await mediator.Send(new GetCalendarIdByCalendarTypeQuery(calendarTypeEnum));

            ErrorOr<MyEventResult> eventInfo = await mediator.Send(new GetEventByIDQuery(accessToken, calendarId.Value, eventId));

            if (eventInfo.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: eventInfo.FirstError.Description);
            }
            else
            {
                return Ok(eventInfo.Value);
            }
        }
    }
}