﻿using Application.User.Commands.Add;
using Application.User.Commands.AddToGoogle;
using Application.User.Commands.Udpate;
using Application.User.Commands.UpdateGoogle;
using Application.User.Common;
using Application.User.Common.Add;
using Application.User.Common.Delele;
using Application.User.Common.Group;
using Application.User.Common.List;
using Application.User.Common.UpdateUser;
using Application.User.DeleteGoogle;
using Application.User.Queries.GetAllGroupsByUserEmail;
using Application.User.Queries.GetByEmail;
using Application.User.Queries.GetById;
using Application.User.Queries.GetGroup;
using Application.User.Queries.List;
using Contract.User;
using Contract.User.Google;
using Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        // get all users
        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            ErrorOr<List<UserResult>> listResult = await mediator.Send(new ListQuery());

            if (listResult.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: listResult.FirstError.Description);
            }

            List<UserResponse> users = new List<UserResponse>();
            foreach (var user in listResult.Value)
            {
                users.Add(mapper.Map<UserResponse>(user));
            }

            return Ok(users);
        }

        // add user to db
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest request)
        {
            var command = mapper.Map<AddUserCommand>(request);

            ErrorOr<UserResult> addResult = await mediator.Send(command);

            return addResult.Match(
                    addResult => Ok(mapper.Map<UserResponse>(addResult)),
                    errors => Problem(errors)
                );
        }

        // update user in db
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserRequest request)
        {
            return await Update(id, request);
        }
        
        // delete user from db
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id, UpdateUserRequest request)
        {
            return await Update(id, request);
        }

        private async Task<IActionResult> Update(string id, UpdateUserRequest request)
        {
            var command = mapper.Map<UpdateUserCommand>((Guid.Parse(id), request));

            ErrorOr<UserResult> updateResult = await mediator.Send(command);

            if (updateResult.IsError && updateResult.FirstError == Errors.UpdateUser.UpdateUserFail)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: updateResult.FirstError.Description);
            }

            return updateResult.Match(
                updateResult => Ok(mapper.Map<UserResponse>(updateResult)),
                errors => Problem(errors));
        }

        // get all google users
        [HttpGet("accessToken")]
        public async Task<IActionResult> GetGoogleUsers(string accessToken)
        {
            ErrorOr<List<GoogleUserRecord>> listResult = await mediator.Send(new ListQueryGoogle(accessToken));

            if (listResult.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: listResult.FirstError.Description);
            }

            List<GoogleUserResponse> users = new List<GoogleUserResponse>();
            foreach (var user in listResult.Value)
            {
                users.Add(mapper.Map<GoogleUserResponse>(user));
            }

            return Ok(users);
        }

        [HttpGet("Email")]
        public async Task<IActionResult> GetGoogleUser(string accessToken, string email)
        {
            ErrorOr<GoogleUserRecord> userResult = await mediator.Send(new GetByEmailQuery(accessToken, email));

            if (userResult.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: userResult.FirstError.Description);
            }

            var user = mapper.Map<GoogleUserResponse>(userResult.Value);

            return Ok(user);
        }

        // update google user
        [HttpPut("accessToken")]
        public async Task<IActionResult> UpdateGoogleUser(UpdateGoogleUserRequest request)
        {
            var command = mapper.Map<UpdateGoogleCommand>((request));

            ErrorOr<UpdateGoogleUserRecord> updateResult = await mediator.Send(command);

            if (updateResult.IsError && updateResult.FirstError == Errors.UpdateGoogle.UpdateGoogleUserFail)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: updateResult.FirstError.Description);
            }

            // write code to return without mapping
            return Ok(updateResult.Value);
        }

        [HttpDelete("accessToken")]
        public async Task<IActionResult> DeleteGoogleUser(string accessToken, string userEmail)
        {
            ErrorOr<DeleteGoogleUserRecord> deleteResult = await mediator.Send(new DeleteGoogleCommand(accessToken, userEmail));

            if (deleteResult.IsError && deleteResult.FirstError == Errors.UpdateGoogle.UpdateGoogleUserFail)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: deleteResult.FirstError.Description);
            }

            // write code to return without mapping
            return Ok(deleteResult.Value);
        }

        [HttpPost("accessToken")]
        public async Task<IActionResult> AddGoogleUser(AddGoogleUserRequest request)
        {
            var command = mapper.Map<AddToGoogleCommand>((request));

            ErrorOr<AddGoogleUserRecord> addResult = await mediator.Send(command);

            if (addResult.IsError && addResult.FirstError == Errors.UpdateGoogle.UpdateGoogleUserFail)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: addResult.FirstError.Description);
            }

            // write code to return without mapping
            return Ok(addResult.Value);
        }

        [HttpGet(nameof(GetGroupByGroupEmail))]
        public async Task<IActionResult> GetGroupByGroupEmail(string accessToken, string groupEmail)
        {
            ErrorOr<GroupResult> groupResult = await mediator.Send(new GetGroupByGroupEmailQuery(accessToken, groupEmail));

            if (groupResult.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "");
            }

            return Ok(groupResult);
        }

        [HttpGet(nameof(GetAllGroupsByUserEmail))]
        public async Task<IActionResult> GetAllGroupsByUserEmail(string accessToken, string userEmail)
        {
            ErrorOr<List<GroupResult>> groupResult = await mediator.Send(new GetAllGroupsByUserEmailQuery(accessToken, userEmail));

            if (groupResult.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "");
            }

            return Ok(groupResult);
        }
    }
}