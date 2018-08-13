/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:53:12 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Flows;
using AutumnBox.GUI.Windows;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("推送文件到手机主目录")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron | Basic.Device.DeviceState.Recovery)]
    public class EFilePusher : AutumnBoxExtension
    {
        public override int Main()
        {
            bool? dialogResult = null;
            string seleFile = null;
            App.RunOnUIThread(() =>
            {
                Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
                fileDialog.Reset();
                fileDialog.Title = App.GetPublicResouce<string>("SelecteAFile");
                fileDialog.Filter = "刷机包/压缩包文件(*.zip)|*.zip|镜像文件(*.img)|*.img|全部文件(*.*)|*.*";
                fileDialog.Multiselect = false;
                dialogResult = fileDialog.ShowDialog();
                seleFile = fileDialog.FileName;
            });

            if (dialogResult == true)
            {
                var args = new FilePusherArgs()
                {
                    DevBasicInfo = TargetDevice,
                    SourceFile = seleFile,
                };
                var pusher = new FilePusher();
                pusher.Init(args);
                pusher.MustTiggerAnyFinishedEvent = true;
                pusher.RunAsync();
                App.RunOnUIThread(() =>
                {
                    new FileSendingWindow(pusher).ShowDialog();
                });
                return OK;
            }
            else
            {
                return ERR;
            }
        }
    }
}
