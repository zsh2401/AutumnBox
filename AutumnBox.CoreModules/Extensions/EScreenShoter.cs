/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:37:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Flows;
using AutumnBox.OpenFramework.Extension;
using System.Windows.Forms;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("截图并保存到电脑")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    public class EScreenShoter : AutumnBoxExtension
    {
        public override int Main()
        {
            DialogResult dialogResult = DialogResult.No;
            string path = null;
            App.RunOnUIThread(() =>
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                dialogResult = fbd.ShowDialog();
                path = fbd.SelectedPath;
            });

            if (dialogResult == DialogResult.OK)
            {
                var shoter = new ScreenShoter();
                shoter.Init(new ScreenShoterArgs()
                {
                    DevBasicInfo = TargetDevice,
                    SavePath = path
                });
                App.ShowLoadingWindow();
                var output = shoter.Run();
                App.CloseLoadingWindow();
                Logger.Info(output.OutputData.ToString());
                return OK;
            }
            return ERR;
        }
    }
}
