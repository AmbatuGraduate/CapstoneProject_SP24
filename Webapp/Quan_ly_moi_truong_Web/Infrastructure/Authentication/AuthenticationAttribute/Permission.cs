using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.AuthenticationAttribute
{
    public static class Permission
    {
        public const string TREE_DEPARTMENT = "quản lý cây xanh";
        public const string GARBAGE_COLLECTION_DEPARTMENT = "quản lý thu gom rác";
        public const string CLEANER_DEPARTMENT = "quản lý quét dọn";
        public const string ADMIN = "admin";
        public const string HR = "hr";
    }
}
