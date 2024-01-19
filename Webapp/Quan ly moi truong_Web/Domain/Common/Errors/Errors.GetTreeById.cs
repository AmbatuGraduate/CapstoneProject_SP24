using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class GetTreeById
        {
            public static Error getTreeFail = Error.NotFound(
                code: "get.GetTreeById", description: "Not found tree");
        }
    }
}