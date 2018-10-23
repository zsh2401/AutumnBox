/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:37:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.OpenFramework.Extension;
using System;
using System.IO;
using System.Windows.Forms;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot
{
    [ExtName("截图并保存到电脑", "en-us:Screenshot and save to pc")]
    [ExtIcon("Icons.screenshot.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    internal class EScreenShoter : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            DialogResult dialogResult = DialogResult.No;
            FileInfo path = null;
            string saveDir = null;
            App.RunOnUIThread(() =>
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                dialogResult = fbd.ShowDialog();
                saveDir = fbd.SelectedPath;
            });
            if (dialogResult == DialogResult.OK)
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                path = new FileInfo(Path.Combine(saveDir, fileName));
                try
                {
                    var capture = GetDeviceCommander<ScreenCapture>();
                    capture.To(OutputPrinter);
                    capture.Capture();
                    capture.SaveToPC(path);
                    return OK;
                }
                catch(Exception ex)
                {
                    Logger.Warn("can't capture",ex);
                    return ERR;
                }
            }
            return ERR;
        }
        protected override bool VisualStop()
        {
            return false;
        }
    }
}
