namespace FluxoDeCaixa.Api.Model
{
    public class JwtSettings
    {
        public JwtSettings()
        {
            ValidateAudience = false;
            ValidateIssuerSigningKey = false;
            ValidateIssuer = true;
            ValidateLifetime = true;
            RequireExpirationTime = false;
            IssuerSigningKey = string.Empty;
            ValidIssuer = string.Empty;
            ValidAudience = string.Empty;

        }
        public bool ValidateIssuerSigningKey { get; set; }

        public string IssuerSigningKey { get; set; }

        public bool ValidateIssuer { get; set; }

        public string ValidIssuer { get; set; }

        public bool ValidateAudience { get; set; }

        public string ValidAudience { get; set; }

        public bool RequireExpirationTime { get; set; }

        public bool ValidateLifetime { get; set; }

    }
}
