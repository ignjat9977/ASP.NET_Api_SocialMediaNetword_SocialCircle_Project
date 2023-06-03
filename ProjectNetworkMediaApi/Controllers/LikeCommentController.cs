using Application;
using Application.Commands;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectNetworkMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeCommentController : ControllerBase
    {

        private readonly IApplicationActor _actor;
        private readonly UseCaseExecutor _executor;

        public LikeCommentController(IApplicationActor actor, UseCaseExecutor executor)
        {
            _actor = actor;
            _executor = executor;
        }

        

        // POST api/<LikeCommentController>
        [HttpPost]
        public IActionResult Post([FromBody] CommentLikeDto dto,
            [FromServices] ICreateLikeCommentCommand command)
        {
            _executor.ExecuteCommand(command, dto);

            return StatusCode(201);
        }

        
    }
}
