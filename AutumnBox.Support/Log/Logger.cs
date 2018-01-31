/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/31 12:12:45 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Support.Log
{
    public static partial class Logger
    {
        public static void D(object senderOrTag, object content) { }
        public static void D(object senderOrTag, object content, Exception e) { }
        public static void T(object senderOrTag, object content) { }
        public static void T(object senderOrTag, object content,Exception e) { }
    }
}
