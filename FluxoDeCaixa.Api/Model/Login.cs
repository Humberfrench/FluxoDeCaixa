using System.ComponentModel.DataAnnotations;

namespace FluxoDeCaixa.Api.Model
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
