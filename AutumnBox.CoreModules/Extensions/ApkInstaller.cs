/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 8:32:48 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using AutumnBox.GUI.Windows;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("安装APK")]
    [ExtAuth("zsh2401")]
    [ExtDesc("Just like that")]
    [ExtIcon("Icons.android.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    public class ApkInstaller : AutumnBoxExtension
    {
        public override int Main()
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = App.GetPublicResouce<string>("SelecteAFile");
            fileDialog.Filter = "安卓安装包ApkFile(*.apk)|*.apk";
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() == true)
            {
                Basic.Flows.ApkInstaller installer = new Basic.Flows.ApkInstaller();
                List<FileInfo> files = new List<FileInfo>();
                foreach (string fileName in fileDialog.FileNames)
                {
                    files.Add(new FileInfo(fileName));
                }
                var args = new ApkInstallerArgs()
                {
                    DevBasicInfo = TargetDevice,
                    Files = files,
                };
                installer.Init(args);
                App.RunOnUIThread(() =>
                {
                    new ApkInstallingWindow(installer, files).ShowDialog();
                });
            }
            else
            {
                return ERR;
            }
            return OK;
        }
    }
}
