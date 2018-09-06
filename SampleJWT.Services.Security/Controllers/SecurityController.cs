
using SampleJWT.Dto;
using SampleJWT.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleJWT.Services.Services.Security.Controllers
{
    public class SecurityController : ApiController
    {
        [HttpPost]
        [ActionName("Login")]
        public HttpResponseMessage Login(LoginDto model)
        {
            //Authentication provider will be injected
            IAuthenticationProvider authenticationProvider = new ADAuthenticationProvider();

            string token, message;
            if(authenticationProvider.Login(model, out token, out message))
            {
                return Request.CreateResponse(HttpStatusCode.OK, token, Configuration.Formatters.JsonFormatter);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, message, Configuration.Formatters.JsonFormatter);
            }
        }

        [HttpPost]
        [Restricted]
        [ActionName("Refresh")]
        public HttpResponseMessage Refresh()
        {
            //Authentication provider will be injected
            IAuthenticationProvider authenticationProvider = new ADAuthenticationProvider();

            //Getting the current token!
            string token = this.ActionContext.Request.Headers.Authorization.Parameter;
            string newToken, message;
            if (authenticationProvider.Refresh(token, out newToken, out message))
            {
                return Request.CreateResponse(HttpStatusCode.OK, newToken, Configuration.Formatters.JsonFormatter);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, message, Configuration.Formatters.JsonFormatter);
            }
        }
    }
}
