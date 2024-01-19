using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class GetTreeTypeById
        {
            public static Error getTreeTypeFail = Error.NotFound(
                code: "get.GetTreeTypeById", description: "Not Found Tree Type");
        }
    }
}