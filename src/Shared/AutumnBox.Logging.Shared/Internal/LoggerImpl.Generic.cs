using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Logging.Internal
{
    class LoggerImpl<TCategory> : LoggerImpl, ILogger<TCategory>
    {
        protected override string CategoryName => typeof(TCategory).Name;
    }
}
