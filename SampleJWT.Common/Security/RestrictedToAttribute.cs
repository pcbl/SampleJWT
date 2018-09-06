using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SampleJWT.Security
{
    public class Restricted : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Roles to restrict access to
        /// </summary>
        public string Roles { get; set; }

        public override void OnAuthorization(HttpActionContext filterContext)
        {

            if (!IsUserAuthorized(filterContext))
            {
                ShowAuthenticationError(filterContext);
                return;
            }
            base.OnAuthorization(filterContext);
        }


        public bool IsUserAuthorized(HttpActionContext actionContext)
        {
            bool toReturn = false;
            //First of all, let´s check for the Authorization Token!
            string requestToken = null;

            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null)
            {
                requestToken = authRequest.Parameter;
            }

            if (requestToken != null)
            {            
                //we got a token!
                //It is time to 
                var tokenManager = new TokenManager();
                JwtSecurityToken userPayloadToken = tokenManager.ValidateToken(requestToken);

                if (userPayloadToken != null)
                {
                    //ticket is valid!                                     
                    var identity = tokenManager.PopulateUserIdentity(userPayloadToken);
                    if (IsWithinExpectedRoles(identity.Roles))
                    {
                        //Now let´s ensure it is within the expected roles!
                        //Let´s set the thread principal to use it
                        var genericPrincipal = new GenericPrincipal(identity, identity.Roles);
                        Thread.CurrentPrincipal = genericPrincipal;
                        toReturn = true;
                    }
                }

            }
            return toReturn;
        }

        private static void ShowAuthenticationError(HttpActionContext filterContext)
        {
            filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            filterContext.Response.Content = new StringContent("Unable to access, Please login again");
        }

        private bool IsWithinExpectedRoles(string[] rolesToCheck)
        {
            //No role specified, we will not check at all!
            if (string.IsNullOrWhiteSpace(Roles)) return true;
            //Otherwise we check...
            var expected= Roles.Split(',');
            return rolesToCheck.Any(role => expected.Contains(role));
        }
    }
}
