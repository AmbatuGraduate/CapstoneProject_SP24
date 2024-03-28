﻿using Application.Calendar;
using Application.Group.Common;
using Application.Group.Common.Add_Update;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Group.Commands.Add
{
    public record AddGroupCommand(string accessToken, AddGoogleGroup group) : IRequest<ErrorOr<GroupResult>>;
}

