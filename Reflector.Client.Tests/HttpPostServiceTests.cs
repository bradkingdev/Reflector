using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Reflector.Client.Services;
using Reflector.Client.Services.Exceptions;
using Reflector.Client.Services.Models;

namespace Reflector.Client.Tests
{

    [TestFixture]
    public class HttpPostServiceTests
    {
        [Test]
        public void DoesTokenAuthInjectorHaveXAuth()
        {
            var request = WebRequest.Create("http://www.fakeurl.com");
            var injector = new TokenAuthenticationInjectorSvc();
            injector.InjectAuthenticationData(request);
            var header = request.Headers["X-Auth-Token"];
            Assert.IsTrue(header != null);
        }

        [Test]
        public void DoesJsonPostServiceReturnValidJson()
        {
            var artifactEnv = new ArtifactEnvelope
            {
                CustomerName = "Brad",
                CustomerNumber = 2
            };

            var jsonSvc = new JsonDataSvc<ArtifactEnvelope>();
            var rzlt = jsonSvc.GetStringRepresentation(artifactEnv);
            Assert.IsTrue(rzlt.Contains("Brad") && rzlt.Contains("2"));
        }

        [Test]

        public void DoesHttpPostSvcThrowExceptionWhenInvalidObject()
        {

            var delegatedMethod = new TestDelegate(() =>
            {
                var jsonSvc = new JsonDataSvc<ArtifactEnvelope>();
                var authInjector = new TokenAuthenticationInjectorSvc();
                var httpSvc = new HttpPostSvc<ArtifactEnvelope>(@"http://www.912912931293129.com", authInjector, jsonSvc);
                httpSvc.MakeRequest(new ArtifactEnvelope());
            });
            Assert.Throws<HttpPostSvcException>(delegatedMethod);
        }


    }
}
