using Reflector.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Reflector.Client.Services
{
    public class HttpGetSvc<T> where T : class
    {
        private readonly JsonDataSvc<T> _dataFormatter;
        public HttpGetSvc()
        {
            _dataFormatter = new JsonDataSvc<T>();
        }

        public T DoGetCall(string url, Dictionary<string, string> arguments) 
        {
            var httpReq = WebRequest.Create(url) as HttpWebRequest;
            httpReq.Method = "GET";
            string data = string.Empty;

            var response = httpReq.GetResponse() as HttpWebResponse;
            using (var datStream = response.GetResponseStream())
            using (var datReader = new StreamReader(datStream))
            {
                data = datReader.ReadToEnd();
            }
            var dataObj = _dataFormatter.GetObjectRepresentation(data) as T;
            return dataObj;
        }
    }
}
