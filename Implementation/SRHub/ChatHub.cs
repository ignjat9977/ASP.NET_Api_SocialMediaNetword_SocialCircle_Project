using Application;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace ProjectNetworkMediaApi.SRHub
{
    public class ChatHub : Hub
    {
        private readonly ConnectionMapping _connectionMapping;
        private readonly IApplicationActor _actor;

        public ChatHub(ConnectionMapping connectionMapping, IApplicationActor
            actor)
        {
            _actor = actor; 
            _connectionMapping = connectionMapping;
        }

        public override Task OnConnectedAsync()
        {
            string token = Context.GetHttpContext().Request.Query["access_token"];

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            string userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            _connectionMapping.Add(new UserConnection
            {
                UserId = userId,
                ConnectionId = Context.ConnectionId
            });

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _connectionMapping.Remove(new UserConnection
            {
                UserId = _actor.Id.ToString(),
                ConnectionId = Context.ConnectionId
            });

            return base.OnDisconnectedAsync(exception);
        }
    }
}
