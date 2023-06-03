namespace ProjectNetworkMediaApi.Core
{
    public class AppSettings
    {
        
        public JwtSettings JwtSettings { get; set; }
    }
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int DurationSeconds { get; set; }
        public string Issuer { get; set; }
    }
}
