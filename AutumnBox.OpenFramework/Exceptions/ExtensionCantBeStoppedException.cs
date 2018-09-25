/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/24 18:10:09 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Exceptions
{
    public class ExtensionCantBeStoppedException : Exception
    {
        public ExtensionCantBeStoppedException(string message, Exception innerException)
        : base(message, innerException)
        {

        }
        public ExtensionCantBeStoppedException(string message)
        : base(message)
        {

        }
    }
}
