/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/25 19:48:02
** filename: DevStatusBitmapGetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Devices;
using AutumnBox.GUI.Resources.Images;
using AutumnBox.GUI.Util;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.Resources
{
    public static class DevStatusBitmapGetter
    {
        private static readonly Assembly currentAssembly;
        private const string rec = "DevStatus/rec.png";
        private const string fastboot = "DevStatus/fastboot.png";
        private const string poweron = "DevStatus/poweron.png";
        private const string nodev = "DevStatus/no_dev.png";
        static DevStatusBitmapGetter() {
            currentAssembly = Assembly.GetExecutingAssembly();
        }
        public static BitmapImage Get(DeviceStatus status) {
            string path = nodev;
            switch (status) {
                case DeviceStatus.Fastboot:
                    path = fastboot;
                    break;
                case DeviceStatus.Recovery:
                    path = rec;
                    break;
                case DeviceStatus.Poweron:
                    path = poweron;
                    break;
            }
            return ImageGetter.Get(path);
        }
    }
}
