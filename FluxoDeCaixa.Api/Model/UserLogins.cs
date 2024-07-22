using System.ComponentModel.DataAnnotations;

namespace FluxoDeCaixa.Api.Model
{
    public class UserLogins
    {
        public UserLogins()
        {
            User = string.Empty;
            Key = string.Empty;
        }

        [Required]
        public string User { get; set; }
        [Required]
        public string Key { get; set; }

    }

}
