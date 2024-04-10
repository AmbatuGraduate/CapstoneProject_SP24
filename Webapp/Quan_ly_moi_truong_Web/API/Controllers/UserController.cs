using Application.GoogleAuthentication.Common;
using Application.GoogleAuthentication.Queries.ExistEmployee;
using Application.GoogleAuthentication.Queries.GoogleAccessToken;
using Application.User.Commands.AddToGoogle;
using Application.User.Commands.DeleteGoogle;
using Application.User.Commands.UpdateGoogle;
using Application.User.Common.Add;
using Application.User.Common.Delele;
using Application.User.Common.List;
using Application.User.Common.UpdateUser;
using Application.User.Queries.GetByEmail;
using Application.User.Queries.List;
using Contract.User.Google;
using Domain.Common.Errors;
using ErrorOr;
using Infrastructure.Authentication.AuthenticationAttribute;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
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
        //[HttpGet]
        //public async Task<IActionResult> ListUsers()
        //{
        //    ErrorOr<List<UserResult>> listResult = await mediator.Send(new ListQuery());

        //    if (listResult.IsError)
        //    {
        //        return Problem(statusCode: StatusCodes.Status400BadRequest, title: listResult.FirstError.Description);
        //    }

        //    List<UserResponse> users = new List<UserResponse>();
        //    foreach (var user in listResult.Value)
        //    {
        //        users.Add(mapper.Map<UserResponse>(user));
        //    }

        //    return Ok(users);
        //}

        //// add user to db
        //[HttpPost]
        //public async Task<IActionResult> AddUser(AddUserRequest request)
        //{
        //    var command = mapper.Map<AddUserCommand>(request);

        //    ErrorOr<UserResult> addResult = await mediator.Send(command);

        //    return addResult.Match(
        //            addResult => Ok(mapper.Map<UserResponse>(addResult)),
        //            errors => Problem(errors)
        //        );
        //}

        //// update user in db
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUser(string id, UpdateUserRequest request)
        //{
        //    return await Update(id, request);
        //}

        //// delete user from db
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(string id, UpdateUserRequest request)
        //{
        //    return await Update(id, request);
        //}

        //private async Task<IActionResult> Update(string id, UpdateUserRequest request)
        //{
        //    var command = mapper.Map<UpdateUserCommand>((Guid.Parse(id), request));

        //    ErrorOr<UserResult> updateResult = await mediator.Send(command);

        //    if (updateResult.IsError && updateResult.FirstError == Errors.UpdateUser.UpdateUserFail)
        //    {
        //        return Problem(statusCode: StatusCodes.Status400BadRequest, title: updateResult.FirstError.Description);
        //    }

        //    return updateResult.Match(
        //        updateResult => Ok(mapper.Map<UserResponse>(updateResult)),
        //        errors => Problem(errors));
        //}

        // get all google users
        [HttpGet("GetGoogleUsers")]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> GetGoogleUsers()
        {
            var clientType = Request.Headers["Client-Type"];

            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }
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

        [HttpGet]
        public async Task<IActionResult> GetGoogleUser(string email)
        {
            var clientType = Request.Headers["Client-Type"];

            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }
            ErrorOr<GoogleUserRecord> userResult = await mediator.Send(new GetByEmailQuery(accessToken, email));

            if (userResult.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: userResult.FirstError.Description);
            }

            var user = mapper.Map<GoogleUserResponse>(userResult.Value);

            return Ok(user);
        }

        // update google user
        [HttpPut]
        public async Task<IActionResult> UpdateGoogleUser(UpdateGoogleUserRequest request)
        {
            var clientType = Request.Headers["Client-Type"];

            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }

            var command = new UpdateGoogleCommand
            (
                accessToken,
                request.Name,
                request.FamilyName,
                request.Email,
                request.Password,
                request.phone,
                request.address,
                request.birthDate,
                request.departmentEmail
            );

            ErrorOr<UpdateGoogleUserRecord> updateResult = await mediator.Send(command);

            if (updateResult.IsError && updateResult.FirstError == Errors.UpdateGoogle.UpdateGoogleUserFail)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: updateResult.FirstError.Description);
            }

            // write code to return without mapping
            return Ok(updateResult.Value);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> DeleteGoogleUser(string userEmail)
        {
            var clientType = Request.Headers["Client-Type"];

            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }

            ErrorOr<DeleteGoogleUserRecord> deleteResult = await mediator.Send(new DeleteGoogleCommand(accessToken, userEmail));

            if (deleteResult.IsError && deleteResult.FirstError == Errors.UpdateGoogle.UpdateGoogleUserFail)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: deleteResult.FirstError.Description);
            }

            // write code to return without mapping
            return Ok(deleteResult.Value);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [HasPermission(Permission.ADMIN)]
        public async Task<IActionResult> AddGoogleUser(AddGoogleUserRequest request)
        {
            var clientType = Request.Headers["Client-Type"];

            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }

            var command = new AddToGoogleCommand
            (
                accessToken,
                request.Name,
                request.FamilyName,
                request.Email,
                request.Password,
                request.phone,
                request.address,
                request.birthDate,
                request.departmentEmail
            );

            ErrorOr<AddGoogleUserRecord> addResult = await mediator.Send(command);

            if (addResult.IsError && addResult.FirstError == Errors.UpdateGoogle.UpdateGoogleUserFail)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: addResult.FirstError.Description);
            }

            // write code to return without mapping
            return Ok(addResult.Value);
        }

        // user is employee in db
        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> IsEmployee(string email)
        {
            ErrorOr<bool> isEmployee = await mediator.Send(new ExistEmployeeQuery(email));

            if (isEmployee.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: isEmployee.FirstError.Description);
            }

            return Ok(isEmployee.Value);
        }
    }
}