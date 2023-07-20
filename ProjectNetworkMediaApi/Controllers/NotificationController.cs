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
    public class NotificationController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public NotificationController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<NotificationController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchDto dto, [FromServices] ISearchAllNotificationsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query,dto));
        }


        // DELETE api/<NotificationController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteNotificationCommand command)
        {
            _executor.ExecuteCommand(command, id);

            return NoContent();
        }
    }
}
