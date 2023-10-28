 namespace Auth.Domain.Entities
{
    public class KeycloakConfig
    {
        public string Realm { get; set; } 
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string BaseUrl { get; set; }

        public string ClientNumber { get; set; } 


        public KeycloakConfig()
        {
            Realm = "cost-tracking-app";
            ClientId = "cost-tracking-client";
            ClientSecret = "O6qyJVLColeu3KnncWrk7NpTyDSvNJZN";
            BaseUrl = "https://lemur-5.cloud-iam.com/auth";
            ClientNumber = "17199bf4-657f-458d-92f5-ceb815451556";
        }
    }
}
