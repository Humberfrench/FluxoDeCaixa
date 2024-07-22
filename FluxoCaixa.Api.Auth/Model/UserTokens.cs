namespace FluxoCaixa.Api.Auth.Model
{
    public class UserTokens
    {
        public UserTokens()
        {
            AccessToken = string.Empty;
            UserName = string.Empty;
            RefreshToken = string.Empty;
            TokenType = string.Empty;
            Issued = string.Empty;
            ExpiraEm = string.Empty;
            CriadoEm = string.Empty;
            ErrorDescription = string.Empty;
            Error = string.Empty;
            Expires = string.Empty;
            Claims = string.Empty;
        }

        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public TimeSpan Validaty { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public string Issued { get; set; }
        public Guid GuidId { get; set; }
        public DateTime ExpiredTime { get; set; }
        public int ExpiresIn { get; set; }
        public string Expires { get; set; }
        public string CriadoEm { get; set; }
        public string ExpiraEm { get; set; }
        public string ErrorDescription { get; set; }
        public int TempoExpiracao { get; set; }
        public string Error { get; set; }
        public string Claims { get; set; }

    }
}
