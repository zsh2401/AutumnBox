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

namespace AutumnBox.OpenFramework.Extension
{
    public class DestoryArgs { }
    public class InitArgs { }
    public class StartArgs
    {
        public DeviceBasicInfo Device { get; set; }
        public string ExtensionPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "AutumnBox_Mods";
            }
        }
    }
}
