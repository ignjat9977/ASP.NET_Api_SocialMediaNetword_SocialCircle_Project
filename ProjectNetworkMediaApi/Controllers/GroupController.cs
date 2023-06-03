using Application;
using Application.Commands;
using Application.Dto;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectNetworkMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private IApplicationActor _actor;
        private UseCaseExecutor _executor;

        public GroupController(IApplicationActor actor, UseCaseExecutor executor)
        {
            _actor = actor;
            _executor = executor;
        }

        //search groups
        // GET: api/<PostsGroupController>

        [HttpGet]
        public IActionResult Get([FromQuery] SearchDto search,[FromServices] ISearchGroupsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        //view group info
        // GET api/<PostsGroupController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetGroupInfoQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }
        
        //create group
        // POST api/<PostsGroupController>
        [HttpPost]
        public IActionResult Post([FromBody] GroupInsertDto dto,
            [FromServices] ICreateGroupCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201);
        }

        //update group info
        // PUT api/<PostsGroupController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GroupInsertDto dto, [FromServices] IUpdateGroupCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<PostsGroupController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteGroupCommand command)
        {
            _executor.ExecuteCommand(command, id);

            return NoContent();

        }
    }
}
