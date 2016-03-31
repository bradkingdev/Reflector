using Reflector.Client.Services.Interfaces;
using Reflector.Client.Services.Models;
using Reflector.Client.Services.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reflector.Client.Services.Repositories
{
    class ArtifactEnvelopeRepository : IHttpGetRepository<ArtifactEnvelope>
    {
        private readonly string _url;

        public ArtifactEnvelopeRepository(string getRequestUrl)
        {
            _url = getRequestUrl;
        }

        public ArtifactEnvelope Get(Dictionary<string, string> args)
        {
            var argDictionary = new Dictionary<string, string>();
            List<ArtifactEnvelope> artifacts = new List<ArtifactEnvelope>();
            var getSvc = new HttpGetSvc<ArtifactEnvelope>();
            return getSvc.DoGetCall(_url, argDictionary);
        }
    }

}
