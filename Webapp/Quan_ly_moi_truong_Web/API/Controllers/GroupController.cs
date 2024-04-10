using Application.Common.Interfaces.Authentication;
using Application.GoogleAuthentication.Common;
using Application.GoogleAuthentication.Queries.GoogleAccessToken;
using Application.Group.Commands.Add;
using Application.Group.Commands.Delete;
using Application.Group.Commands.Update;
using Application.Group.Common;
using Application.Group.Common.Add_Update;
using Application.Group.Queries.GetAllGroups;
using Application.Group.Queries.GetAllGroupsByUserEmail;
using Application.Group.Queries.GetAllMembersOfGroup;
using Application.Group.Queries.GetGroup;
using Application.User.Common.List;
using ErrorOr;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Infrastructure.Authentication.AuthenticationAttribute;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    [EnableCors("AllowAllHeaders")]
    public class GroupController : ApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        private readonly HttpClient _httpClient;
        private readonly ISessionService _sessionService;

        // constructor
        public GroupController(IMediator mediator, IMapper mapper, IHttpClientFactory httpClientFactory, ISessionService sessionService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            _httpClient = httpClientFactory.CreateClient();
            _sessionService = sessionService;
        }

        [HttpGet()]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> GetGroupByGroupEmail(string groupEmail)
        {
            string accessToken;
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
            ErrorOr<GroupResult> groupResult = await mediator.Send(new GetGroupByGroupEmailQuery(accessToken, groupEmail));

            if (groupResult.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "");
            }

            return Ok(groupResult);
        }

        [HttpGet()]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> GetAllMembersOfGroup(string groupEmail)
        {
            string accessToken;
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
            ErrorOr<List<GoogleUser>> usersResult = await mediator.Send(new GetAllMembersOfGroupQuery(accessToken, groupEmail));

            if (usersResult.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "");
            }

            return Ok(usersResult);
        }

        [HttpGet()]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> GetAllGroups()
        {
            ErrorOr<List<GroupResult>> groupResult = await mediator.Send(new GetAllGroupsQuery());

            if (groupResult.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "");
            }

            return Ok(groupResult.Value);
        }

        [HttpGet()]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN + "," + Permission.GARBAGE_COLLECTION_DEPARTMENT + "," + Permission.CLEANER_DEPARTMENT)]
        public async Task<IActionResult> GetAllGroupsByUserEmail(string userEmail)
        {
            string accessToken;
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
            ErrorOr<List<GroupResult>> groupResult = await mediator.Send(new GetAllGroupsByUserEmailQuery(accessToken, userEmail));

            if (groupResult.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "");
            }

            return Ok(groupResult);
        }

        [HttpPost()]
        [Authorize(Roles = "Admin")]
        [HasPermission(Permission.ADMIN)]
        public async Task<IActionResult> AddGroup(AddGoogleGroup group)
        {
            string accessToken;
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
            ErrorOr<GroupResult> groupResult = await mediator.Send(new AddGroupCommand(accessToken, group));

            if (groupResult.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "");
            }

            return Ok(groupResult);
        }

        [HttpPost()]
        [Authorize(Roles = "Admin")]
        [HasPermission(Permission.ADMIN)]
        public async Task<IActionResult> UpdateGroup(UpdateGoogleGroup group)
        {
            string accessToken;
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
            ErrorOr<GroupResult> groupResult = await mediator.Send(new UpdateGroupCommand(accessToken, group));

            if (groupResult.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "");
            }

            return Ok(groupResult);
        }

        [HttpDelete()]
        [Authorize(Roles = "Admin")]
        [HasPermission(Permission.ADMIN)]
        public async Task<IActionResult> DeleteGroup(string groupEmail)
        {
            string accessToken;
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
            ErrorOr<bool> groupResult = await mediator.Send(new DeleteGroupCommand(accessToken, groupEmail));

            if (groupResult.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "");
            }

            return Ok(groupResult);
        }
    }
}