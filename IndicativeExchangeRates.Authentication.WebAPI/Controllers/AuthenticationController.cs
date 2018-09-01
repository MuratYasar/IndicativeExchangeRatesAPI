using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace IndicativeExchangeRates.Authentication.WebAPI.Controllers
{    
    public class AuthenticationController : ApiController
    {
        [BasicAuthentication]
        public HttpResponseMessage Get()
        {
            string usernametoken = Thread.CurrentPrincipal.Identity.Name;  
            
            if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                HttpError myCustomError = new HttpError("You must autheticate yourself.");
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, myCustomError);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, usernametoken);
            }
        }
    }
}
