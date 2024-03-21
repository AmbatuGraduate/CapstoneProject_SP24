using Application.User.Common.Delele;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.DeleteGoogle
{
    public record DeleteGoogleCommand
    (
      string accessToken,
      string userEmail
    ) : IRequest<ErrorOr<DeleteGoogleUserRecord>>;
}
