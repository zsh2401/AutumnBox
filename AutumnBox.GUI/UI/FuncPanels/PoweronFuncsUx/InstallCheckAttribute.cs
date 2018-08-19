/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/17 18:24:39 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.PackageManage;
using AutumnBox.GUI.Util.UI;
using AutumnBox.Support.Log;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.GUI.UI.FuncPanels.PoweronFuncsUx
{
    public class InstallCheckAttribute : FuncsPrecheckAttribute
    {
        public string ErrorMsgKey { get; set; } = "PlzInstallAppFirst";
        public string PkgName { get; private set; }
        public InstallCheckAttribute(string pkgName)
        {
            this.PkgName = pkgName;
        }
        public override bool Check(DeviceBasicInfo tragetDevice)
        {
            Logger.Info(this, "install checking");
            bool isInstall = false;
            Task.Run(() =>
            {
                Thread.Sleep(500);
                isInstall = PackageManager.IsInstall(tragetDevice, PkgName) == true;
                BoxHelper.CloseLoadingDialog();
            });
            BoxHelper.ShowLoadingDialog();
            if (!isInstall)
            {
                BoxHelper.ShowMessageDialog("Warning", ErrorMsgKey);
            }
            return isInstall;
        }
    }
}
