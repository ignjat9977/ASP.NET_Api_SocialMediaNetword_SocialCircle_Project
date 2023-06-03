using Application;

namespace ProjectNetworkMediaApi.Core
{
    public class JwtActor : IApplicationActor
    {
        public int Id { get; set; }

        public string Identity { get; set; }

        public IEnumerable<int> AllowedUseCases { get; set; }
        public string Role { get; set; }
    }
}
