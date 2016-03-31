using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reflector.Client.Services.Exceptions
{

    [Serializable]
    public class HttpPostSvcException : Exception
    {
        private const string exMsg = "Unable to make HTTP Post via HTTP Post Svc";
        public HttpPostSvcException() { }
        public HttpPostSvcException(Exception inner) : base(exMsg  , inner) { }
        protected HttpPostSvcException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
