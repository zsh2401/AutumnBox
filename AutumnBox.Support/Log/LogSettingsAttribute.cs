/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/14 20:34:33 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Support.Log
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class LogSettingsAttribute : Attribute
    {
        internal const string DEFAULT_FILENAME = "default.log";
        internal const bool DEFAULT_DEBUG_MODE_SETTING = true;
        public string FileName { get; set; } = DEFAULT_FILENAME;
        public bool IsInDebugMode { get; set; } = DEFAULT_DEBUG_MODE_SETTING;
    }
}
