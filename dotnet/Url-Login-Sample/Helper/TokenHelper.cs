using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Url_Login_Sample.Helper
{
    public static class TokenHelper
    {
        private static readonly string _ValidIssuer = "Ede.Uofx.Url.Sample";
        private static readonly string _ValidAudience = "Uofx Url.Sample";
        private static readonly string _IssuerSigningKey = "thisisaverysecurekeythatisatleast256bitslong!";
        private static readonly string _UofClaimDataKey = "uofdata";

        public static string GenToken(string data, DateTimeOffset expireDate)
        {
            var claim = new Claim[]
                {
                    new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
                    new Claim(_UofClaimDataKey, data),
                    new Claim(JwtRegisteredClaimNames.Exp, expireDate.ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString())
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_IssuerSigningKey));
            var token = new JwtSecurityToken
                (
                    issuer: _ValidIssuer,
                    audience: _ValidAudience,
                    claims: claim,
                    expires: expireDate.DateTime,
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            var newToken = $"{new JwtSecurityTokenHandler().WriteToken(token)}";
            return newToken;
        }

        public static bool VirtyfyAndGetData(string token, out string data)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_IssuerSigningKey));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _ValidIssuer,
                ValidateAudience = true,
                ValidAudience = _ValidAudience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                var jsonToken = validatedToken as JwtSecurityToken;
                data = jsonToken.Claims.FirstOrDefault(r => r.Type == _UofClaimDataKey)?.Value;
                return true;
            }
            catch
            {
                data = null;
                return false;
            }
        }
    }
}
