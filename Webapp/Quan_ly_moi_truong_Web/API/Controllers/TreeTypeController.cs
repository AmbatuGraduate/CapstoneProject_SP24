using Application.TreeType.Commands.Add;
using Application.TreeType.Commands.Update;
using Application.TreeType.Common;
using Application.TreeType.Queries.GetById;
using Application.TreeType.Queries.List;
using Contract.TreeType;
using Domain.Common.Errors;
using ErrorOr;
using Infrastructure.Authentication.AuthenticationAttribute;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class TreeTypeController : ApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public TreeTypeController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
/*        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN)]*/
        public async Task<IActionResult> Get()
        {
            ErrorOr<List<TreeTypeResult>> list = await mediator.Send(new ListTreeTypeQuery());

            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            List<ListTreeTypeResponse> treeTypes = new List<ListTreeTypeResponse>();
            foreach (var treeType in list.Value)
            {
                treeTypes.Add(mapper.Map<ListTreeTypeResponse>(treeType));
            }

            return Ok(treeTypes);
        }

        [HttpGet("{TreeTypeId}")]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN)]
        public async Task<IActionResult> GetById(string TreeTypeId)
        {
            var query = mapper.Map<GetByIdQuery>(Guid.Parse(TreeTypeId));

            ErrorOr<TreeTypeResult> result = await mediator.Send(query);

            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
            }

            return Ok(mapper.Map<ListTreeTypeResponse>(result.Value));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN)]
        public async Task<IActionResult> Add(AddTreeTypeRequest request)
        {
            var command = mapper.Map<AddTreeTypeCommand>(request);

            ErrorOr<TreeTypeResult> addResult = await mediator.Send(command);

            return addResult.Match(
                treeToAdd => Ok(mapper.Map<ListTreeTypeResponse>(addResult)),
                errors => Problem(errors)
                );
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin, Manager")]
        [HasPermission(Permission.TREE_DEPARTMENT + "," + Permission.ADMIN)]
        public async Task<IActionResult> Update(string Id, UpdateTreeTypeRequest request)
        {
            var command = mapper.Map<UpdateTreeTypeCommand>((Guid.Parse(Id), request));

            ErrorOr<TreeTypeResult> updateResult = await mediator.Send(command);

            if (updateResult.IsError && updateResult.FirstError == Errors.GetTreeTypeById.getTreeTypeFail)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: updateResult.FirstError.Description);
            }

            return updateResult.Match(
                updateResult => Ok(mapper.Map<ListTreeTypeResponse>(updateResult)),
                errors => Problem(errors));
        }
    }
}