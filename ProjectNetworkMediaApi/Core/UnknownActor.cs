using Application;

namespace ProjectNetworkMediaApi.Core
{
    public class UnknownActor : IApplicationActor
    {
        public int Id => 0;

        public string Identity => "Unknown Actor";


        public IEnumerable<int> AllowedUseCases => new List<int> { 8 };
    }
}
