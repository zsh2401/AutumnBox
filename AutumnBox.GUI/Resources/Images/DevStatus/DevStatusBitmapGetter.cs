/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/25 19:48:02
** filename: DevStatusBitmapGetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.GUI.Resources.Images;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.Resources
{
    internal static class DevStatusBitmapGetter
    {
        private static readonly Assembly currentAssembly;
        private const string rec = "DevStatus/rec.png";
        private const string fastboot = "DevStatus/fastboot.png";
        private const string poweron = "DevStatus/poweron.png";
        private const string nodev = "DevStatus/no_dev.png";
        static DevStatusBitmapGetter() {
            currentAssembly = Assembly.GetExecutingAssembly();
        }
        public static BitmapImage Get(DeviceState status) {
            string path = nodev;
            switch (status) {
                case DeviceState.Fastboot:
                    path = fastboot;
                    break;
                case DeviceState.Recovery:
                    path = rec;
                    break;
                case DeviceState.Poweron:
                    path = poweron;
                    break;
            }
            return ImageGetter.Get(path);
        }
    }
}
