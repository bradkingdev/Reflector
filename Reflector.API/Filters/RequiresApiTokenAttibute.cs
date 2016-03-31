using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Configuration;

namespace Reflector.API
{
    public class RequiresApiTokenAttribute : AuthorizationFilterAttribute
    {
        
        
        public override bool AllowMultiple
        {
            get { return false; }
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            IEnumerable<string> values;
            actionContext.Request.Headers.TryGetValues("X-Auth-Token", out values);

            if (values == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            else 
            {
                string headerToken = values.First();
                var requiredToken = ConfigurationManager.AppSettings["RequiredToken"];

                if (headerToken != requiredToken)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}