/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:30:08 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.V1
{
    public class DestoryArgs { }
    public class InitArgs { }
    public class RunArgs
    {
        public DeviceBasicInfo Device { get; set; }
    }
}
