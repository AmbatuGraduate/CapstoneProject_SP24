using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class GetScheduleTreeTrimById
        {
            public static Error getScheduleFail = Error.NotFound(
                               code: "get.GetScheduleTreeTrimById", description: "Schedule not found");
        }
    }
}