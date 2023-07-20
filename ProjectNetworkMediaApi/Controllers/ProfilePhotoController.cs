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
    public class ProfilePhotoController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public ProfilePhotoController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<ProfilePhotoController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchDto dto, [FromServices] IGetAllProfilePhotosQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, dto));
        }


        // PUT api/<ProfilePhotoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromServices] ISetProfilePhotoActiveCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }

        // DELETE api/<ProfilePhotoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteProfilePhotoCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
