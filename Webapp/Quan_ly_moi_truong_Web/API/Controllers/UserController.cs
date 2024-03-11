using Application.User.Commands.Add;
using Application.User.Commands.Udpate;
using Application.User.Common;
using Application.User.Queries.List;
using Contract.User;
using Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.User.Queries.ListBySchedule;
using Application.ScheduleTreeTrim.Common;

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserRequest request)
        {
            return await Update(id, request);
        }

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

        // Get schedules for a user
        [HttpGet("{id}/schedules")]
        public async Task<IActionResult> GetSchedulesByUserId(string id)
        {
            var query = new UserScheduleQuery(Guid.Parse(id));
            ErrorOr<List<ScheduleTreeTrimResult>> result = await mediator.Send(query);

            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
            }

            return Ok(result.Value);
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
    }
}