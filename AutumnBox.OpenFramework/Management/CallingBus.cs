/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/7 20:55:48 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Management
{
#if SDK
    internal 
#else
    public
#endif

        static class CallingBus
    {
        public static IAutumnBox_GUI AutumnBox_GUI { get; private set; }
        public static void LoadApi(IAutumnBox_GUI atmbGui)
        {
            AutumnBox_GUI = atmbGui ?? throw new ArgumentNullException(nameof(atmbGui));
        }
    }
}
