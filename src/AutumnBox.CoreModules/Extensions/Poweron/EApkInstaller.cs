/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 8:32:48 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("安装APK", "en-us:Install APK")]
    [ExtDesc("可直接向手机安装APK,不过要注意允许USB安装哦!", "en-us:Install apk to device")]
    [ExtIcon("Icons.android.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ClassText("title", "Select a apk file", "zh-cn:选择一个APK")]
    [ClassText("filter", "Android APK file(*.apk)|*.apk", "zh-cn:安卓APK(*.apk)|*.apk")]
    [ClassText("waiting", "waiting for user", "zh-cn:等待用户")]
    [ClassText("status_fmt", "Installing {0}/{1}", "zh-cn:正在安装{0}/{1}个Apk")]
    [ClassText("result_fmt", "Finished {0} success {1} error   {2} total", "zh-cn:完成 {0}个成功 {1}个失败 共{2}个")]
    internal class EApkInstaller : LeafExtensionBase
    {
        [AutoInject]
        ICommandExecutor executor;

        [AutoInject]
        IDevice device;

        [AutoInject]
        ILeafUI ui;

        [LMain]
        public void EntryPoint()
        {
            using (ui)
            {
                ui.Show();
                Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
                fileDialog.Reset();
                fileDialog.Title = this.RxGetClassText("title");
                fileDialog.Filter = this.RxGetClassText("filter");
                fileDialog.Multiselect = true;
                executor.OutputReceived += (s, e) => ui.WriteLineToDetails(e.Text);
                ui.WriteLineToDetails(this.RxGetClassText("waiting"));
                if (fileDialog.ShowDialog() != true) ui.EShutdown();
                List<FileInfo> files = new List<FileInfo>();
                foreach (string fileName in fileDialog.FileNames)
                {
                    files.Add(new FileInfo(fileName));
                }
                Install(files);
                ui.Finish(StatusMessages.Success);
            }
        }

        private void Install(List<FileInfo> files)
        {
            int successed = 0;
            int error = 0;
            int currentInstalling = 1;
            int totalCount = files.Count;
            SetTip(currentInstalling, totalCount);
            foreach (var file in files)
            {
                try
                {
                    var result = executor.Adb(device, $"install -r -t -d \"{file.FullName}\"");
                    if (result.ExitCode == 0)
                    {
                        successed++;
                    }
                    else
                    {
                        error++;
                    }
                }
                catch (Exception ex)
                {
                    SLogger<EApkInstaller>.Warn("install apk failed", ex);
                    error++;
                }
                if (currentInstalling < totalCount)
                {
                    currentInstalling++;
                    SetTip(currentInstalling, totalCount);
                }
            };
            string fmtString = this.RxGetClassText("result_fmt");
            ui.StatusDescription = (string.Format(fmtString, successed, error, totalCount));
        }

        private void SetTip(double crt, double total)
        {
            ui.StatusInfo = string.Format(this.RxGetClassText("status_fmt"), crt, total);
            ui.WriteLineToDetails(string.Format(this.RxGetClassText("status_fmt"), crt, total));
        }
    }
}
