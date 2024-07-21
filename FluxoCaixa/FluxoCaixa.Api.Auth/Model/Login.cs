using System.ComponentModel.DataAnnotations;

namespace FluxoCaixa.Api.Auth.Model
{
    public class Login
    {
        public Login()
        {
            ClientUser = "";
            ClientSecret = "";
        }

        [Required]
        public string ClientUser { get; set; }
        [Required]
        public string ClientSecret { get; set; }

    }
}
