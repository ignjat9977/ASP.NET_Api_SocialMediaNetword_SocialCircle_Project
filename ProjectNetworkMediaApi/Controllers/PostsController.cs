using Application;
using Application.Commands;
using Application.Dto;
using Application.Queries;
using AutoMapper;
using Bogus;
using DataAcess;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectNetworkMediaApi.Core.Extensions;
using ProjectNetworkMediaApi.Core.Validators;
using ProjectNetworkMediaApi.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectNetworkMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public PostsController(IApplicationActor actor, UseCaseExecutor executor)
        {
            this.actor = actor;
            this.executor = executor;
        }



        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchPostDto search,[FromServices] ISearchPostUserWallQuery query)
        {
            
            return Ok(executor.ExecuteQuery(query, search));
            

           
        }
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        // POST api/<ValuesController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] PostDto dto,
            [FromServices] ICreatePostCommand command)
        {

            executor.ExecuteCommand(command, dto);

            return StatusCode(201);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PostDto dto,
            [FromServices] IUpdatePostCommand command)
        {

            executor.ExecuteCommand(command, dto);

            return NoContent();

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeletePostCommand command)
        {
            
            executor.ExecuteCommand(command, id);
            return NoContent();

        }

    }
}
