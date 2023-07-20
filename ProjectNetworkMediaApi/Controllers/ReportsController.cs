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
    public class ReportsController : ControllerBase
    {

        private readonly UseCaseExecutor _executor;

        public ReportsController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<ReportsController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchReportsDto dto, [FromServices] ISearchReportsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, dto));
        }


        // POST api/<ReportsController>
        [HttpPost]
        public IActionResult Post([FromBody] ReportDto dto, [FromServices] ICreateReportCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201);
        }

        
    }
}
