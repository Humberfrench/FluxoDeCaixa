using Dietcode.Core.Lib;
using FluxoCaixa.Api.Auth.Authority;
using FluxoCaixa.Api.Auth.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FluxoCaixa.Api.Lancamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings jwtSettings;
        public AuthController(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(UserTokens), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Autenticação / Obter Token da Api")]
        public IActionResult Post(Login login)
        {
            if ((login.ClientUser == "french") && (login.ClientSecret == "frenchx1"))
            {
                var Token = JwtHelpers.GenTokenkey(new UserTokens()
                {
                    GuidId = Guid.NewGuid(),
                    UserName = login.ClientUser,
                }, jwtSettings);
                Token.Claims = JwtHelpers.GetClaims(Token, Token.GuidId).ToJson();

                return Ok(Token);
            }

            //erro
            var problem = new ProblemDetails
            {
                Detail = "Usuário ou chave inválidos.",
                Status = 401,
                Title = "Erro na autenticação",
                Type = "https://httpstatuses.com/401",
                Instance = "",
            };

            return Unauthorized(problem);
        }

    }
}
