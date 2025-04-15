using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class GetCultivarById
        {
            public static Error getCultivarById = Error.NotFound(code: "get.GetCultivarById", description: "Cultivar Not Found");
        }
    }
}