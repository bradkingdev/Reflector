using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Reflector.Client.Services.Exceptions;
using Reflector.Client.Services.Interfaces;

namespace Reflector.Client.Services
{
    public class HttpPostSvc<T>
    {
        private readonly string _url;
        private readonly IPostAuthenticationInjector _authInjector;
        private readonly IHttpDataFormatter<T> _postDataSvc;
        public HttpPostSvc(string url, IPostAuthenticationInjector authInjector, IHttpDataFormatter<T> postDataSvc)
        {
            _url = url;
            _authInjector = authInjector;
            _postDataSvc = postDataSvc;
        }

        public string MakeRequest(T postData)
        {
            try
            {
                var data = _postDataSvc.GetStringRepresentation(postData);
                var request = (HttpWebRequest)WebRequest.Create(_url);

                makeHttpHeader(request, _authInjector);
                makeHttpBody(data, request);

                StreamReader reader = getHttpResponse(request);
                var returnData = reader.ReadToEnd();
                return reader.ReadToEnd();
            }
            catch(Exception ex)
            {
                throw new HttpPostSvcException(ex);
            }
        }

        internal StreamReader getHttpResponse(HttpWebRequest request)
        {
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            return reader;
        }

        internal void makeHttpBody(string data, HttpWebRequest request)
        {
            StreamWriter requestWriter;

            using (requestWriter = new StreamWriter(request.GetRequestStream()))
            {
                requestWriter.Write(data);
            }
        }

        internal void makeHttpHeader(HttpWebRequest request, IPostAuthenticationInjector authProvider)
        {
            
            request.Method = "POST";
            authProvider.InjectAuthenticationData(request);
            request.ServicePoint.Expect100Continue = false;
            request.Timeout = 20000;
            request.ContentType = "application/json";
        }
    }
}
