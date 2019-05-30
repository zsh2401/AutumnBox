/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 8:32:48 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("安装APK", "en-us:Install APK")]
    [ExtDesc("可直接向手机安装APK,不过要注意允许USB安装哦!", "en-us:Install apk to device")]
    [ExtIcon("Icons.android.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtText("title", "Select a apk file", "zh-cn:选择一个APK")]
    [ExtText("filter", "Android APK file(*.apk)|*.apk", "zh-cn:安卓APK(*.apk)|*.apk")]
    [ExtText("waiting", "waiting for user", "zh-cn:等待用户")]
    [ExtText("status_fmt", "Installing {0}/{1}", "zh-cn:正在安装{0}/{1}个Apk")]
    [ExtText("result_fmt", "Finished {0} success {1} error   {2} total", "zh-cn:完成 {0}个成功 {1}个失败 共{2}个")]
    internal class EApkInstaller : LeafExtensionBase
    {
        private readonly CommandExecutor executor = new CommandExecutor();
        [LProperty]
        public IDevice Device { get; set; }
        [LProperty]
        public ILeafUI UI { get; set; }
        [LProperty]
        public IClassTextManager Text { get; set; }
        [LMain]
        public void EntryPoint()
        {
            using (UI)
            {
                UI.Title = this.GetName();
                UI.Icon = this.GetIconBytes();
                UI.Show();
                Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
                fileDialog.Reset();
                fileDialog.Title = Text["title"];
                fileDialog.Filter = Text["filter"];
                fileDialog.Multiselect = true;
                UI.WriteLine(Text["waiting"]);
                if (fileDialog.ShowDialog() != true) UI.EShutdown();
                List<FileInfo> files = new List<FileInfo>();
                foreach (string fileName in fileDialog.FileNames)
                {
                    files.Add(new FileInfo(fileName));
                }
                Install(files);
                UI.Finish(0);
            }
        }

        private void Install(List<FileInfo> files)
        {
            executor.To(e => UI.WriteOutput(e.Text));
            int successed = 0;
            int error = 0;
            int currentInstalling = 1;
            int totalCount = files.Count;
            SetTip(currentInstalling, totalCount);
            foreach (var file in files)
            {
                try
                {
                    var result = executor.Adb(Device, $"install -r -t -d \"{file.FullName}\"");
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
            string fmtString = Text["result_fmt"];
            UI.WriteLine(string.Format(fmtString, successed, error, totalCount));
        }

        private void SetTip(double crt, double total)
        {
            UI.Tip = string.Format(Text["status_fmt"], crt, total);
            UI.WriteOutput(string.Format(Text["status_fmt"], crt, total));
        }
    }
}
