using Application.Notification.Commands.Add;
using Application.Notification.Common;
using Application.Notification.Queries.List;
using Application.Notification.Queries.ListByUsername;
using Contract.Notification;
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
    public class NotificationController : ApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public NotificationController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        // get all notification
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ErrorOr<List<NotificationResult>> list = await mediator.Send(new ListNotificationQuery());

            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            List<ListNotificationResponse> notifications = new List<ListNotificationResponse>();
            foreach (var notification in list.Value)
            {
                notifications.Add(mapper.Map<ListNotificationResponse>(notification));
            }

            return Ok(notifications);
        }

        // get list of notification by username
        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            ErrorOr<List<NotificationResult>> list = await mediator.Send(new ListNotificationByUsernameQuery(username));

            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            List<ListNotificationResponse> notifications = new();
            foreach (var notification in list.Value)
            {
                notifications.Add(mapper.Map<ListNotificationResponse>(notification));
            }

            return Ok(notifications);
        }

        // create a notification
        [HttpPost]
        public async Task<IActionResult> Create(AddNotificationRequest addNotificationRequest)
        {
            var command = mapper.Map<AddNotificationCommand>(addNotificationRequest);
            ErrorOr<NotificationResult> notification = await mediator.Send(command);

            if (notification.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: notification.FirstError.Description);
            }

            return notification.Match(
                notification => Ok(mapper.Map<ListNotificationResponse>(notification)),
                errors => Problem(errors));
        }
    }
}