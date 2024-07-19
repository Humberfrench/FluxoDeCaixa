using FluxoCaixa.Api.Auth.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FluxoCaixa.Api.Auth.Authority
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            return new Claim[] {
                new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, userAccounts.UserName),
                    new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
        }

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return userAccounts.GetClaims(Id);
        }

        public static UserTokens GenTokenkey(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var UserToken = new UserTokens();
                if (model == null) throw new ArgumentException(nameof(model));
                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                Guid Id = Guid.Empty;
                DateTime hoje = DateTime.UtcNow;
                DateTime expireTime = hoje.AddDays(1);
                UserToken.Validaty = expireTime.TimeOfDay;
                var JWToken = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer, audience: jwtSettings.ValidAudience, claims: model.GetClaims(out Id), notBefore: new DateTimeOffset(DateTime.Now).DateTime, expires: new DateTimeOffset(expireTime).DateTime, signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                UserToken.AccessToken = new JwtSecurityTokenHandler().WriteToken(JWToken);
                UserToken.UserName = model.UserName;
                UserToken.GuidId = Id;
                UserToken.TempoExpiracao = 24 * 60; // in minutes
                UserToken.Expires = (24 * 60).ToString(); // in minutes
                UserToken.ExpiresIn = 24 * 60; // in minutes
                UserToken.ExpiredTime = expireTime;
                UserToken.CriadoEm = hoje.ToString("dd/MM/yyyy HH:mm:ss");
                UserToken.ExpiraEm = expireTime.ToString("dd/MM/yyyy HH:mm:ss");
                UserToken.Issued = "TOKEN_BEARER";
                UserToken.TokenType = "Bearer";
                return UserToken;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
