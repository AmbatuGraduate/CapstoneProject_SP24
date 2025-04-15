using Application.Calendar;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Schedules;
using Application.User.Common.Delele;
using ErrorOr;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Application.User.Commands.DeleteGoogle
{
    public class DeleteGoogleHandler : IRequestHandler<DeleteGoogleCommand, ErrorOr<DeleteGoogleUserRecord>>
    {
        private readonly IUserRepository userRepository;
        private readonly ITreeCalendarService treeCalendarService;

        public DeleteGoogleHandler(IUserRepository userRepository, ITreeCalendarService treeCalendarService)
        {
            this.userRepository = userRepository;
            this.treeCalendarService = treeCalendarService;
        }

        public async Task<ErrorOr<DeleteGoogleUserRecord>> Handle(DeleteGoogleCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask; 
            var currentUserGroupEmail = userRepository.GetCurrentDepartmentOfUser(request.userEmail);

            // get user by user email
            var user = await userRepository.GetGoogleUserByEmail(request.accessToken, request.userEmail);

            // filter calendar that have the user need to delete
            var calendarId = treeCalendarService.GetCalendarIdByCalendarType(Domain.Enums.CalendarTypeEnum.CayXanh);
            var calendarByUser = await treeCalendarService.GetEventsByAttendeeEmail(request.accessToken, calendarId, request.userEmail);

            // remove user from calendar
            foreach (var calendar in calendarByUser)
            {
                var attendee = calendar.Attendees.SingleOrDefault(x => x.FullName == user.Name);
                if (attendee != null)
                {
                    calendar.Attendees.Remove(attendee);
                }
            }

            // update calendar
            foreach (var calendar in calendarByUser)
            {
                var updateStartTime = new EventDateTime
                {
                    DateTime = calendar.Start.ToString(),
                };
                var updateEndTime = new EventDateTime
                {
                    DateTime = calendar.End.ToString(),
                };

                // list of new attendees in calendar
                List<Calendar.User> users = new List<Calendar.User>();
                foreach(var attendee in calendar.Attendees)
                {
                    users.Add(new Calendar.User
                    {
                        Name = attendee.FullName,
                        Email = attendee.user.Email
                    });
                }

                var updateCalendar = new MyUpdatedEvent
                {
                    Summary = calendar.Summary,
                    Description = calendar.Description,
                    Location = calendar.Location,
                    Start = updateStartTime,
                    End = updateEndTime, 
                    Attendees = users
                };
                var update = await treeCalendarService.UpdateEvent(request.accessToken, calendarId, updateCalendar, calendar.Id);
            }

                var userResult = await userRepository.DeleteGoogleUser(request.accessToken, request.userEmail);

            if (userResult && !currentUserGroupEmail.IsNullOrEmpty())
            {
                await userRepository.RemoveUserFromGoogleGroup(request.accessToken, request.userEmail, currentUserGroupEmail);
                await userRepository.RemoveUserFromDBGroup(currentUserGroupEmail);
            }

            return new DeleteGoogleUserRecord(userResult);
        }
    }
}