using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reflector.Client.Services.Interfaces
{
    public interface IHttpDataFormatter<T>
    {
        string GetStringRepresentation(T data);
        T GetObjectRepresentation(string data);

    }
}
