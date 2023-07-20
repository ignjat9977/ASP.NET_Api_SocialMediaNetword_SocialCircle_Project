using Application;
using Application.Commands;
using Application.Dto;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;
using ProjectNetworkMediaApi.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectNetworkMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static IEnumerable<string> AllowedExtensionsPhoto =>
             new List<string> { ".jpg", ".png", ".jpeg" };

        private readonly UseCaseExecutor _executor;

        public UserController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchDto dto, [FromServices] ISearchUsersQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, dto));
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromForm] UploadProfileImageDto dto,
            [FromServices] ICreateProfileImageCommand command)
        {
            UploadDto dtoForDB = new UploadDto();
            if (dto.Image != null)
            {
                var guid = Guid.NewGuid().ToString();

                var extension = Path.GetExtension(dto.Image.FileName);

                if (!AllowedExtensionsPhoto.Contains(extension))
                {
                    throw new InvalidOperationException("Unsupported file type.");
                } 



                var fileName = guid + extension;

                var filePath = Path.Combine("wwwroot", "images", fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                dto.Image.CopyTo(stream);


                dtoForDB.ImageFileName = fileName;
                dtoForDB.UserId = dto.UserId;
            }
            _executor.ExecuteCommand(command, dtoForDB);

            return StatusCode(201);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDto dto , [FromServices] IUpdateUserInfoCommand command)
        {
            _executor.ExecuteCommand(command , dto);

            return NoContent();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,
            [FromServices] IDeleteUserCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
    public class UploadProfileImageDto
    {
        public int UserId { get; set; }
        public IFormFile Image { get; set; }
    }
}
