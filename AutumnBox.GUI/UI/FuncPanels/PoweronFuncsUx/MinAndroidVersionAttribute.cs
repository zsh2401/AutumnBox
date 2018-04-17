/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/17 18:27:01 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.GUI.Helper;
using AutumnBox.Support.Log;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.GUI.UI.FuncPanels.PoweronFuncsUx
{
    public class MinAndroidVersion : FuncsPrecheckAttribute
    {
        private Version version;
        public MinAndroidVersion(int major, int minor = 0, int build = 0)
        {
            version = new Version(major, minor, build);
        }
        public override bool Check(DeviceBasicInfo targetDevice)
        {
            Logger.Debug(this, "android version checking");
            Version result = new Version(1, 0);
            Task.Run(() => {
                Thread.Sleep(1000);
                result = new DeviceBuildPropGetter(targetDevice.Serial).GetAndroidVersion();
                BoxHelper.CloseLoadingDialog();
            });
            BoxHelper.ShowLoadingDialog();
            Logger.Debug(this,$"Min{version} Device{result}");
            if (result < version)
            {
                string tooLowFmt = App.Current.Resources["msgAndroidVersionTooLowFmt"].ToString();
                string msg = String.Format(tooLowFmt, version.ToString(3));
                BoxHelper.ShowMessageDialog("Warning", msg);
                return false;
            }
            return true;
        }
    }
}
