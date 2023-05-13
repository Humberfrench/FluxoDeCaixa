namespace FluxoDeCaixa.Api.Model
{
    public class UserTokens
    {
        public UserTokens()
        {
            Token = string.Empty;
            UserName = string.Empty;
            RefreshToken = string.Empty;
            Email = string.Empty;

        }

        public string Token { get; set; }
        public string UserName { get; set; }
        public TimeSpan Validaty { get; set; }
        public string RefreshToken { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public Guid GuidId { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
