using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SampleJWT.Security
{
    public class TokenManager
    {
        //To create a key:
        //openssl rsa -in jwtRS256.key -pubout -outform PEM -out jwtRS256.key.pub
        private const string communicationKey = "UsVfdlvXZg22jcsEAKQEqf6GporrUcEqzeN+v+WOjoY";
        SecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(communicationKey));

        // The Method is used to generate token for user
        public string GenerateTokenForUser(CustomUserIdentity user)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(communicationKey));
            var now = DateTime.UtcNow;
            var signingCredentials = new SigningCredentials(signingKey,
               SecurityAlgorithms.HmacSha256, SecurityAlgorithms.HmacSha256);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Sid, user.Sid)               
            }, "Custom");

            //Adding roles!
            claimsIdentity.AddClaims(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = "self",
                Audience = "http://www.pcbl.de",
                Subject = claimsIdentity,
                SigningCredentials = signingCredentials,
                Expires = DateTime.Now.AddMinutes(1)                
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
            var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);

            return signedAndEncodedToken;

        }

        /// Using the same key used for signing token, user payload is generated back
        public JwtSecurityToken ValidateToken(string authToken)
        {

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudiences = new string[]
                      {
                    "http://www.pcbl.de",
                      },

                ValidIssuers = new string[]
                  {
                      "self",
                  },
                IssuerSigningKey = signingKey,
                ValidateLifetime = true
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken validatedToken = null;

            try
            {
                tokenHandler.ValidateToken(authToken, tokenValidationParameters, out validatedToken);                
            }
            catch (Exception ex)
            {
                //Log here the reason if needed!

            }

            return validatedToken as JwtSecurityToken;
        }

        public CustomUserIdentity PopulateUserIdentity(JwtSecurityToken userPayloadToken)
        {
            string name = ((userPayloadToken)).Claims.FirstOrDefault(m => m.Type == "unique_name").Value;
            string sid = ((userPayloadToken)).Claims.FirstOrDefault(m => m.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;
            string[] roles = ((userPayloadToken)).Claims.Where(m => m.Type == "role").Select(m => m.Value).ToArray();
            var toReturn = new CustomUserIdentity(name) { Sid = sid, Roles=roles };
            return toReturn;

        }
    }
}
