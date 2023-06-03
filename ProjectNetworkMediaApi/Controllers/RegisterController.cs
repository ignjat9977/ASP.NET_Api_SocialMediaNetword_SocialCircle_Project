using Application;
using Application.Commands;
using AutoMapper;
using DataAcess;
using Domain;
using Microsoft.AspNetCore.Mvc;
using ProjectNetworkMediaApi.Core.Extensions;
using ProjectNetworkMediaApi.Core.Validators;
using ProjectNetworkMediaApi.Dto;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectNetworkMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public RegisterController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        

        // POST api/<RegisterController>
        [HttpPost]
        public IActionResult Post([FromBody] UserDto dto,
            [FromServices] ICreateUserCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201);
        }

    }
}
