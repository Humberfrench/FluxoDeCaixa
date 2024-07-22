using FluxoDeCaixa.Api.Model;
using Dietcode.Core.DomainValidator;
using Dietcode.Api.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using FluxoDeCaixa.Api.Authority;

namespace FluxoDeCaixa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Autenticação/Token")]
    public class AuthController : ApiControllerBase
    {
        private readonly JwtSettings jwtSettings;
        public AuthController(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        [AllowAnonymous, HttpPost("")]
        [ProducesResponseType(typeof(UserTokens), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Autenticação / Obter Token da Api")]
        public async Task<IActionResult> Post(UserLogins userLogins)
        {
            try
            {
                //Implementando auth Fake. Ou seja um user e key fixos.
                //Caso haja necessidade de implementar um auth real, seria necessario uma tabela com a chave criptografada.
                // eu crio o certificado no server, master key e a chave de criptografia.
                // estou pulando este passo por enquanto. Apenas para deixar a api funcional. PARA TESTE.


                if (!string.Equals(userLogins.User, "dietcode", StringComparison.CurrentCultureIgnoreCase))
                {
                    var retorno = new ProblemDetails
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Detail = "Não foi possivel fazer o login. Dados inválidos",
                        Title = "Não encontrado.",
                        Type = "Erro",
                        Instance = "Autenticação/Token"
                    };
                    return BadRequest(retorno);
                }

                if (!string.Equals(userLogins.Key, "edocteid", StringComparison.CurrentCultureIgnoreCase))
                {
                    var retorno = new ProblemDetails
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Detail = "Não foi possivel fazer o login. Dados inválidos",
                        Title = "Não encontrado.",
                        Type = "Erro",
                        Instance = "Autenticação/Token"
                    };
                    return BadRequest(retorno);
                }

                var Token = JwtHelpers.GenTokenkey(new UserTokens()
                {
                    Email = "dietcode@dietcode.com.br",
                    GuidId = Guid.NewGuid(),
                    UserName = "dietcode",
                    Id = 1,
                }, jwtSettings);

                return Ok(Token);
            }
            catch (Exception)
            {
                var retorno = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Não foi possivel fazer o login. Dados inválidos",
                    Title = "Não encontrado.",
                    Type = "Erro",
                    Instance = "Autenticação/Token"
                };
                return BadRequest(retorno);
            }
        }
    }
}
