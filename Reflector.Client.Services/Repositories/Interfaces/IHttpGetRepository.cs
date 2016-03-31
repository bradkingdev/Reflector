using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reflector.Client.Services.Repositories.Interfaces
{
    public interface IHttpGetRepository<T> 
    {
        T Get(Dictionary<string, string> args);
    }
}
