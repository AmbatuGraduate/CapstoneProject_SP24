using Application;
using Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreesController : ControllerBase
    {
        private readonly ITreeService _treeService;

        public TreesController(ITreeService treeService)
        {
            _treeService = treeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var treesFromService = _treeService.GetAllTrees();
            return Ok(treesFromService);
        }

        [HttpPost]
        public IActionResult Add(Tree tree)
        {
            var treeToAdd = _treeService.CreateTree(tree);
            return Ok(treeToAdd);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _treeService.DeleteTree(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Tree tree)
        {
            var treeToUpdate = _treeService.UpdateTree(tree);
            return Ok(treeToUpdate);
        }
    }
}
