using Application.GoogleAuthentication.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GoogleAuthentication.Queries.GoogleAccessToken
{
    public record GoogleAccessTokenQuery(string jwt) : IRequest<ErrorOr<GoogleAccessTokenResult>>;
}
