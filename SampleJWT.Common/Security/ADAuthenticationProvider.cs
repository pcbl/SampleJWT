using SampleJWT.Dto;
using SampleJWT.Model;
using SampleJWT.Model.Data;
using SampleJWT.Repository;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleJWT.Security
{
    public class ADAuthenticationProvider : IAuthenticationProvider
    {
        /// <summary>
        /// Authenticates the user and return the token
        /// </summary>
        /// <param name="information">Login Information to validate</param>
        /// <param name="token">Generated Token. Will be empty when Authentication did not worked(Function will return false)</param>
        /// <param name="message">When False, additional information about why token could not be generated</param>
        /// <returns>The true if token was generated, otherwise false. When False, additional information will be on message parameter</returns>
        public bool Login(LoginDto information,out string token, out string message)
        {
            bool toReturn = false;
            token = string.Empty;
            message = string.Empty;
            // create a "principal context" - e.g. your domain (could be machine, too)
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, information.Domain))
            {
                // validate the credentials
                toReturn = pc.ValidateCredentials(information.UserName, information.Password, ContextOptions.Negotiate);
                if (toReturn)
                {
                    //User is vallid, let´s retrieve some more AD Information, if needed
                    var adUser = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, $"{information.Domain}\\{information.UserName}");
                    
                    //this will be injected!
                    IRepository<UserRole> userRoleRepository = new DapperRepository<UserRole>();
                    var userRoles = userRoleRepository.Get(DapperExtensions.Predicates.Field<UserRole>(userRole => userRole.UserName, DapperExtensions.Operator.Eq
                        , information.UserName));
                    //Here we should retrieve access information from DB and store on ticket, as much as possible
                    CustomUserIdentity user = new CustomUserIdentity(information.UserName)
                    {
                        Sid = adUser.Sid.ToString(),
                        Roles = userRoles.Select(item=>item.Role).ToArray()
                    };
                    TokenManager tokenManager = new TokenManager();
                    token = tokenManager.GenerateTokenForUser(user);
                }
                else
                {
                    message = "Invalid user";
                }
            }
            return toReturn;
        }

        public bool Refresh(string existingToken, out string newToken, out string message)
        {
            bool toReturn = false;
            newToken = string.Empty;
            message = string.Empty;

            TokenManager tokenManager = new TokenManager();
            JwtSecurityToken userPayloadToken = tokenManager.ValidateToken(existingToken);
            if(userPayloadToken!=null)
            {
                //restoring identity information:
                var currentIdentity = tokenManager.PopulateUserIdentity(userPayloadToken);
                newToken = tokenManager.GenerateTokenForUser(currentIdentity);
                toReturn = true;
            }
            else
            {
                message = "Invalid Token. Please login again!";
            }
            return toReturn;
        }
    }
}
