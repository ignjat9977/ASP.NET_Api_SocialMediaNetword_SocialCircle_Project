using DataAcess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectNetworkMediaApi.Core
{
    public class JwtManager
    {
        private readonly MyDbContext _context;

        private readonly string _issuer;
        private readonly int _seconds;
        private readonly ITokenStorage _storage;
        private readonly string _secretKey;

        public JwtManager(
            MyDbContext context,
            string issuer,
            string secretKey,
            int seconds,
            ITokenStorage storage)
        {
            _context = context;
            _issuer = issuer;
            _secretKey = secretKey;
            _seconds = seconds;
            _storage = storage;
        }

        public string MakeToken(string email, string password)
        {
            var user = _context.Users.Include(x=>x.Role).ThenInclude(x=>x.RoleUseCases).FirstOrDefault(x => x.Email == email);

            if (user == null || user.Role == null || !user.Role.isActive)
            {
                throw new UnauthorizedAccessException();
            }

            var valid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!valid)
            {
                throw new UnauthorizedAccessException();
            }



            var actor = new JwtActor
            {
                Id = user.Id,
                AllowedUseCases = user.Role.RoleUseCases.Select(x => x.RoleUseCaseId),
                Identity = user.Email,
                Role = user.Role.Name
            };
            var tokenId = Guid.NewGuid().ToString();
            _storage.AddToken(tokenId);
            var claims = new List<Claim> // Jti : "", 
            {
                new Claim(JwtRegisteredClaimNames.Jti, tokenId, ClaimValueTypes.String, _issuer),
                new Claim(JwtRegisteredClaimNames.Iss, _issuer, ClaimValueTypes.String, _issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _issuer),
                new Claim("UserId", actor.Id.ToString(), ClaimValueTypes.String, _issuer),
                new Claim("ActorData", JsonConvert.SerializeObject(actor), ClaimValueTypes.String, _issuer)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddSeconds(_seconds),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    
    }
}
