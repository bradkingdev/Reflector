using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Reflector.Client.Services.Interfaces;

namespace Reflector.Client.Services
{
    public class TokenAuthenticationInjectorSvc : IPostAuthenticationInjector
    {
        public void InjectAuthenticationData(WebRequest webRequest)
        {
            webRequest.Headers.Add("X-Auth-Token", ConfigurationManager.AppSettings["RequiredToken"]);
        }
    }
}
