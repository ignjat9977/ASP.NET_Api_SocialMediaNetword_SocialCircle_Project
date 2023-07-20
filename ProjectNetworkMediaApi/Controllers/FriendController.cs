using Application;
using Application.Commands;
using Application.Dto;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectNetworkMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {

        private readonly IApplicationActor _actor;
        private readonly UseCaseExecutor _executor;

        public FriendController(IApplicationActor actor, UseCaseExecutor executor)
        {
            _actor = actor;
            _executor = executor;
        }


        // GET: api/<FriendController>
        [HttpGet]
        public IActionResult Get([FromQuery] UserIdDto dto,
            [FromServices] IGetFriendsAndFriendsOfFriendsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, dto));
        }

        // GET api/<FriendController>/5
        //Get info about one user
        [HttpGet("{id}")]
        public IActionResult Get(int id,
            [FromServices] IGetUserInfoQuery query)
        {

            return Ok(_executor.ExecuteQuery(query, id));

        }

        // POST api/<FriendController>
        [HttpPost]
        public IActionResult Post([FromBody] FriendDto dto,
            [FromServices] ICreateFriendCommand command)
        {
            _executor.ExecuteCommand(command, dto);

            return StatusCode(201);
        }


        // DELETE api/<FriendController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,
            [FromServices] IDeleteFriendCommand command)
        {

            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
