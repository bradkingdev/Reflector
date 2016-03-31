using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Reflector.Client.Services.Interfaces
{
    public interface IPostAuthenticationInjector
    {
        void InjectAuthenticationData(WebRequest webRequest);
    }
}
