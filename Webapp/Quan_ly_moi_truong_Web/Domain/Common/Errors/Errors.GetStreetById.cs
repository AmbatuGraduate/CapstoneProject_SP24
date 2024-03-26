using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class GetStreetById
        {
            public static Error getStreetFail = Error.NotFound(
                code: "get.GetStreetById", description: "Street not found");
        }
    }
}