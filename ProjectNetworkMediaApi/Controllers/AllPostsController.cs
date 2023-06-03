using Application;
using Application.Dto;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectNetworkMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllPostsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public AllPostsController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<AllPostsController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchDto dto, [FromServices] IGetAllPostsOfUserFriendsAndGroupsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, dto));
        }

    }
}
