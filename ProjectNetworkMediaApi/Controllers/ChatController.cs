using Application;
using Application.Dto;
using Application.Queries;
using DataAcess;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjectNetworkMediaApi.Core;
using ProjectNetworkMediaApi.SRHub;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectNetworkMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ConnectionMapping _connectionMapping;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly MyDbContext _context;
        private readonly UseCaseExecutor _executor;

        public ChatController(ConnectionMapping connectionMapping, IHubContext<ChatHub> hubContext, MyDbContext context, UseCaseExecutor executor)
        {
            _connectionMapping = connectionMapping;
            _hubContext = hubContext;
            _context = context;
            _executor = executor;
        }
        [HttpGet]
        public IActionResult GetInboxParts([FromQuery] SearchDto dto, [FromServices] ISearchInboxPartsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, dto));
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto dto)
        {
            var connectionId = _connectionMapping.GetConnectionForUser(dto.ReciverId);

            
            int id = int.Parse(dto.SenderId);
            var sender = _context.Users.Find(id);

            if (connectionId != null)
            {
                try
                {
                    await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", $"{sender.FirstName} {sender.LastName}", dto);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
            int rid = int.Parse(dto.ReciverId);
            int sid = int.Parse(dto.SenderId);
            var mess = new Message
            {
                ReciverId = rid,
                SenderId = sid,
                Content = dto.Message
            };
            _context.Messages.Add(mess);
            _context.SaveChanges();
            
            return Ok(dto);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetSpecificInboxPartQuery query)
        {
            return Ok(_executor.ExecuteQuery(query,id));
        }
    }
    public class MessageDto
    {
        public string SenderId { get; set; }
        public string ReciverId { get; set; }
        public string Message { get; set; }
    }


}
