using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services.Impl
{
    public abstract class LoggingObject
    {
        protected ILogger Logger { get; }
        public LoggingObject()
        {
            Logger = LoggerFactory.Auto(this.GetType().Name);
        }
    }
}
