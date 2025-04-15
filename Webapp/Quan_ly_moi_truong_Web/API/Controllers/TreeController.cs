
using Application.Tree.Commands.Add;
using Application.Tree.Commands.AutoUpdate;
using Application.Tree.Commands.Delete;
using Application.Tree.Commands.Update;
using Application.Tree.Common;
using Application.Tree.Queries.GetById;
using Application.Tree.Queries.GetByTreeCode;
using Application.Tree.Queries.List;
using Application.Tree.Queries.ListCut;
using Contract.Tree;
using Domain.Common.Errors;
using ErrorOr;
using Infrastructure.Authentication.AuthenticationAttribute;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("AllowAllHeaders")]
    //[AllowAnonymous]
    public class TreeController : ApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TreeController(IMediator mediator, IMapper mapper,IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
/*        [Authorize(Roles = "Admin, Manager, Hr")]
        [HasPermission(Permission.TREE_DEPARTMENT+ ","+Permission.ADMIN + ","+Permission.HR)]*/
        public async Task<IActionResult> Get()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["u_tkn"];

            ErrorOr<List<TreeResult>> list = await mediator.Send(new ListTreeQuery());

            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            List<ListTreeResponse> trees = new List<ListTreeResponse>();
            foreach (var tree in list.Value)
            {
                trees.Add(mapper.Map<ListTreeResponse>(tree));
            }

            return Ok(trees);
        }

        [HttpGet("{TreeId}")]
/*        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN)]*/
        public async Task<IActionResult> GetById(string TreeId)
        {
            var query = mapper.Map<GetByIdQuery>(Guid.Parse(TreeId));

            ErrorOr<TreeDetailResult> result = await mediator.Send(query);

            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
            }
            return Ok(mapper.Map<DetailTreeResponse>(result.Value));
        }

        [HttpGet("{TreeCode}")]
/*        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN)]*/
        public async Task<IActionResult> GetByTreeCode(string TreeCode)
        {
            var query = mapper.Map<GetByTreeCodeQuery>(TreeCode);

            ErrorOr<TreeDetailResult> result = await mediator.Send(query);

            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
            }

            return Ok(mapper.Map<DetailTreeResponse>(result.Value));
        }

        [HttpGet("{Address}")]
/*        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN)]*/
        public async Task<IActionResult> GetCut(string Address)
        {
            if (!_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("u_tkn", out var token))
            {
                return Problem(statusCode: StatusCodes.Status204NoContent, title: "Cookie is null");
            }


            ErrorOr<List<TreeResult>> list = await mediator.Send(new ListTreeCutQuery(token, Address));

            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            List<ListTreeResponse> trees = new List<ListTreeResponse>();
            foreach (var tree in list.Value)
            {
                trees.Add(mapper.Map<ListTreeResponse>(tree));
            }
            return Ok(trees);
        }

        [HttpPost]
/*        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN)]*/
        public async Task<IActionResult> Add(AddTreeRequest request)
        {
            var command = mapper.Map<AddTreeCommand>(request);

            ErrorOr<AddTreeResult> addResult = await mediator.Send(command);

            return addResult.Match(
                treeToAdd => Ok(mapper.Map<AddTreeResponse>(addResult)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("{TreeCode}")]
/*        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN)]*/
        public async Task<IActionResult> Delete(string TreeCode)
        {
            var command = mapper.Map<DeleteTreeCommand>(TreeCode);

            ErrorOr<AddTreeResult> deleteResult = await mediator.Send(command);

            return deleteResult.Match(
                result => Ok(),
                errors => Problem(errors));
        }

        [HttpPut("{TreeCode}")]
/*        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN)]*/
        public async Task<IActionResult> Update(string TreeCode, UpdateTreeRequest request)
        {
            var command = mapper.Map<UpdateTreeCommand>((TreeCode, request));

            ErrorOr<AddTreeResult> updateResult = await mediator.Send(command);

            if (updateResult.IsError && updateResult.FirstError == Errors.GetTreeById.getTreeFail)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: updateResult.FirstError.Description);
            }

            return updateResult.Match(
                updateResult => Ok(mapper.Map<AddTreeResponse>(updateResult)),
                errors => Problem(errors));
        }

        [HttpPut]
        public async Task<IActionResult> AutoUpdate()
        {
            var command = mapper.Map<AutoUpdateTreeCommand>(new AutoUpdateTreeCommand());

            ErrorOr<List<AddTreeResult>> autoUpdateResult = await mediator.Send(command);

            if (autoUpdateResult.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: autoUpdateResult.FirstError.Description);
            }

            List<ListTreeResponse> trees = new List<ListTreeResponse>();
            foreach (var tree in autoUpdateResult.Value)
            {
                trees.Add(mapper.Map<ListTreeResponse>(tree));
            }

            return Ok(trees);
        }
    }
}