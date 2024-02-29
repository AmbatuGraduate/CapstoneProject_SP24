using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class GoogleApiSettings
    {
        public const string SectionName = "GoogleSettings";
        public string ClientId { get; init; }
        public string ClientSecret { get; init; }

    }
}
