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
    public class GroupMembersController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public GroupMembersController(UseCaseExecutor executor)
        {
            _executor = executor;
        }


        // GET api/<GroupMembersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetAllGroupMembersByIdQuery query)
        {
            return Ok(_executor.ExecuteQuery(query,id));
        }

        // POST api/<GroupMembersController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateGroupMemberDto dto, [FromServices] ICreateGroupMemberCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201);

        }
        

        // DELETE api/<GroupMembersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteGroupMemberCommand command)
        {
            _executor.ExecuteCommand(command,id);
            return NoContent();
        }
        
    }
}
