

namespace Application.User.Common
{
    public record CalendarUser
    (
        CalendarUserResult CalendarUserResult
    );

    public class CalendarUserResult
    {
        public string Id { get; set; }
        public string UserCode { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string? DepartmentId { get; set; }
        public string FullName { get; set; }
    }
}
