 namespace Auth.Domain.Entities
{
    public class KeycloakConfig
    {
        public string Realm { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string BaseUrl { get; set; }

        public string ClientNumber { get; set; }
    }
}
