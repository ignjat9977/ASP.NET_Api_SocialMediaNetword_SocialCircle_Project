using Application;
using Application.Commands;
using Application.Dto;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectNetworkMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostGroupController : ControllerBase
    {
        public static IEnumerable<string> AllowedExtensionsPhoto =>
            new List<string> { ".jpg", ".png", ".jpeg" };

        public static IEnumerable<string> AllowedExtensionsVideo =>
            new List<string> { ".mp4", ".mov", ".wmv" };

        private readonly IApplicationActor _actor;
        private readonly UseCaseExecutor _executor;

        public PostGroupController(IApplicationActor actor, UseCaseExecutor executor)
        {
            _actor = actor;
            _executor = executor;
        }

        // GET: api/<PostGroupController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchPostInGroupDto dto,
            [FromServices] ISearchPostInGroupQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, dto));
        }

        // POST api/<PostGroupController>
        [HttpPost]
        public IActionResult Post([FromForm] GroupPostDto dto, [FromServices] ICreatePostGroupPostCommand command)
        {
            Application.Dto.GroupPostDto dtoForDB = new Application.Dto.GroupPostDto();
            if (dto.File != null)
            {
                var guid = Guid.NewGuid().ToString();

                var extension = Path.GetExtension(dto.File.FileName);
                if (dto.VideoOrPhoto == true)
                {

                    if (!AllowedExtensionsPhoto.Contains(extension))
                    {
                        throw new InvalidOperationException("Unsupported file type.");
                    }
                }
                else if (dto.VideoOrPhoto == false)
                {
                    if (!AllowedExtensionsVideo.Contains(extension))
                    {
                        throw new InvalidOperationException("Unsupported file type.");
                    }
                }

                var fileName = guid + extension;

                var filePath = Path.Combine("wwwroot", "images", fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                dto.File.CopyTo(stream);


                dtoForDB.Path = fileName;
                dtoForDB.UserId = dto.UserId;
                dtoForDB.PrivacyId = dto.PrivacyId;
                dtoForDB.Content = dto.Content;
                dtoForDB.Title = dto.Title;
                dtoForDB.GroupId = dto.GroupId;
                dtoForDB.VideoOrPhoto = dto.VideoOrPhoto;
            }
            else
            {
                dtoForDB.UserId = dto.UserId;
                dtoForDB.PrivacyId = dto.PrivacyId;
                dtoForDB.Content = dto.Content;
                dtoForDB.Title = dto.Title;
                dtoForDB.GroupId = dto.GroupId;
            }
            
            _executor.ExecuteCommand(command, dtoForDB);
            return StatusCode(201);
        }

    }
    public class GroupPostDto
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int PrivacyId { get; set; }

        //null classic post
        public IFormFile? File { get; set; }
        //null classic post
        //true photo
        //false video
        public bool? VideoOrPhoto { get; set; }
    }
}
