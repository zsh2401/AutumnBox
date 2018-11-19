using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Exceptions
{
    class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException(string serviceName) : base(serviceName + "not found")
        {
            ServiceName = serviceName;
        }

        public string ServiceName { get; }
    }
}
