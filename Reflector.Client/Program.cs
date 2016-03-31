using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using Reflector.Client.Services;
using Reflector.Client.Services.Models;

namespace Reflector.Client
{
    class Program
    {
        private static string PostUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["RelayPostPath"];
            }
        }

        private static string GetUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["RelayGetPath"];
            }
        }

        private static ArtifactEnvelope data()
        {
            return new ArtifactEnvelope
            {
                Label = "ConsoleAPP",
                Identifer = 999,
                Facts = new List<ReportableFact>
                {
                    new ReportableFact
                    {
                        Classification = "TEST",
                        Descriptor = "FROM CONSOLE APP",
                        Label = "TESTLBL",
                        ActionDateTime = DateTimeOffset.Now
                    },
                    new ReportableFact
                    {
                        Classification = "TEST2",
                        Descriptor = "FROM CONSOLE APP",
                        Label = "TESTLBL",
                        ActionDateTime = DateTimeOffset.Now
                    },
                }
            };
        }

        static void Main(string[] args)
        {
            ArtifactEnvelope envelope = data();
            Console.WriteLine(getHttpResponse(envelope, PostUrl));
            Console.ReadLine();
        }

        private static ArtifactEnvelope makeRequest(Dictionary<string,string> args)
        {
            var getSvc = new HttpGetSvc<ArtifactEnvelope>();
            return getSvc.DoGetCall(GetUrl, args);
        }

        private static string getHttpResponse(ArtifactEnvelope envelope, string url)
        {
            var jsonProvider = new JsonDataSvc<ArtifactEnvelope>();
            var tokenProvider = new TokenAuthenticationInjectorSvc();
            var httpSvc = new HttpPostSvc<ArtifactEnvelope>(url, tokenProvider, jsonProvider);
            return httpSvc.MakeRequest(envelope);
        }

    }
}
