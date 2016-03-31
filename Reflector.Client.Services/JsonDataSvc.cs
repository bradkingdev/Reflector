using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reflector.Client.Services.Interfaces;

namespace Reflector.Client.Services
{
    public class JsonDataSvc<T> : IHttpDataFormatter<T>
    {
        public string GetStringRepresentation(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public T GetObjectRepresentation(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

    }
}
