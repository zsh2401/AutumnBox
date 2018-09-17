using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Util.Debugging
{
    internal interface ILogger
    {
        void Debug(object content);
        void Info(object content);
        void Warn(object content);
        void Warn(object content, Exception ex);
        void Warn(Exception ex);
        void Fatal(object content);
    }
}
