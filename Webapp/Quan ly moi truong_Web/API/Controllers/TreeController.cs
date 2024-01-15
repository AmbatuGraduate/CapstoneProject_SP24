using Application.Tree.Commands.Add;
using Application.Tree.Commands.Delete;
using Application.Tree.Commands.Update;
using Application.Tree.Common;
using Application.Tree.Queries.GetById;
using Application.Tree.Queries.List;
using Contract.Tree;
using Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [EnableCors("AllowAllHeaders")]
    public class TreeController : ApiController
    {

        private readonly IMediator mediator;
        private readonly IMapper mapper;
        public TreeController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ErrorOr<List<TreeResult>> list = await mediator.Send(new ListTreeQuery());

            if (list.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: list.FirstError.Description);
            }

            List<TreeResponse> trees = new List<TreeResponse>();
            foreach (var tree in list.Value)
            {
                trees.Add(mapper.Map<TreeResponse>(tree));
            }

            return Ok(trees);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = mapper.Map<GetByIdQuery>(id);

            ErrorOr<TreeResult> result = await mediator.Send(query);

            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
            }

            return Ok(mapper.Map<TreeResponse>(result.Value));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTreeRequest request)
        {
            var command = mapper.Map<AddTreeCommand>(request);

            ErrorOr<TreeResult> addResult = await mediator.Send(command);

            return addResult.Match(
                treeToAdd => Ok(mapper.Map<TreeResponse>(addResult)),
                errors => Problem(errors)
                );
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = mapper.Map<DeleteTreeCommand>(id);

            ErrorOr<TreeResult> deleteResult = await mediator.Send(command);

            return deleteResult.Match(
                result => Ok(),
                errors => Problem(errors));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateTreeRequest request)
        {
            var command = mapper.Map<UpdateTreeCommand>((id, request));

            ErrorOr<TreeResult> updateResult = await mediator.Send(command);

            if (updateResult.IsError && updateResult.FirstError == Errors.GetTreeById.getTreeFail)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: updateResult.FirstError.Description);
            }

            return updateResult.Match(
                updateResult => Ok(mapper.Map<TreeResponse>(updateResult)),
                errors => Problem(errors));
        }
    }
}
