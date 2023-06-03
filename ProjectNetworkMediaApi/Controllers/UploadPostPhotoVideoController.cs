using Application;
using Application.Commands;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectNetworkMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadPostPhotoVideoController : ControllerBase
    {
        public static IEnumerable<string> AllowedExtensionsPhoto =>
             new List<string> { ".jpg", ".png", ".jpeg" };

        public static IEnumerable<string> AllowedExtensionsVideo =>
            new List<string> { ".mp4", ".mov", ".wmv" };

        private readonly IApplicationActor _actor;
        private readonly UseCaseExecutor _executor;

        public UploadPostPhotoVideoController(IApplicationActor actor, UseCaseExecutor executor)
        {
            _actor = actor;
            _executor = executor;
        }

        [HttpPost]
        public IActionResult Post([FromForm] UploadPhotoOrVideoDto dto,
            [FromServices] ICreatePhotoOrVideoCommand command)
        {
            UploadDto dtoForDB = new UploadDto();
            if (dto.Image != null)
            {
                var guid = Guid.NewGuid().ToString();

                var extension = Path.GetExtension(dto.Image.FileName);

                if(dto.VideoOrPhoto == true)
                {
                    if (!AllowedExtensionsPhoto.Contains(extension))
                    {
                        throw new InvalidOperationException("Unsupported file type.");
                    }
                }
                else
                {
                    if (!AllowedExtensionsVideo.Contains(extension))
                    {
                        throw new InvalidOperationException("Unsupported file type.");
                    }
                }
                
                

                var fileName = guid + extension;

                var filePath = Path.Combine("wwwroot", "images", fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                dto.Image.CopyTo(stream);


                dtoForDB.ImageFileName = fileName;
                dtoForDB.UserId = dto.UserId;
                dtoForDB.PrivacyId = dto.PrivacyId;
                dtoForDB.Content = dto.Content;
                dtoForDB.Title = dto.Title;
                dtoForDB.PhotoOrVIdeo = dto.VideoOrPhoto;
            }
            _executor.ExecuteCommand(command, dtoForDB);
            return StatusCode(201);
        }
    }
    public class UploadPhotoOrVideoDto
    {
        public IFormFile Image { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public int PrivacyId { get; set; }
        public bool VideoOrPhoto { get; set; }
    }
}
