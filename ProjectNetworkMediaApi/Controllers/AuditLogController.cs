using Application;
using Application.Dto;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectNetworkMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {

        private readonly UseCaseExecutor _executor;

        public AuditLogController(UseCaseExecutor executor)
        {
            _executor = executor;
        }




        // GET: api/<AuditLogController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchAuditLogDto dto, [FromServices] ISearchAuditLogQuery query)
        {

            return Ok(_executor.ExecuteQuery(query, dto));
        }

        
    }
}
