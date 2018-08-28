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

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot
{
    [ExtName("安装APK")]
    [ExtName("Install APK", Lang = "en-US")]
    [ExtDesc("可直接向手机安装APK,不过要注意允许USB安装哦!")]
    [ExtDesc("Install apk to device", Lang = "en-US")]
    [ExtIcon("Icons.android.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    public class EApkInstaller : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = App.GetPublicResouce<string>("SelecteAFile");
            fileDialog.Filter = "安卓安装包ApkFile(*.apk)|*.apk";
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() == true)
            {
                List<FileInfo> files = new List<FileInfo>();
                foreach (string fileName in fileDialog.FileNames)
                {
                    files.Add(new FileInfo(fileName));
                }
                Install(files);
            }
            else
            {
                return ERR;
            }
            return OK;
        }

        ApkInstaller installer;

        private void Install(List<FileInfo> files)
        {
            int successed = 0;
            int error = 0;
            int currentInstalling = 1;
            int totalCount = files.Count;

            installer = new ApkInstaller();
            var args = new ApkInstallerArgs()
            {
                DevBasicInfo = TargetDevice,
                Files = files,
            };
            installer.AApkIstanlltionCompleted += (s, e) =>
            {
                if (e.IsSuccess)
                {
                    successed++;
                }
                else
                {
                    error++;
                    Logger.Warn(e.Output.ToString());
                }
                if (currentInstalling < totalCount)
                {
                    currentInstalling++;
                    SetTip(currentInstalling, totalCount);
                }
                return true;
            };
            WriteLine(App.GetPublicResouce<string>("ExtensionIniting"));
            installer.Init(args);
            SetTip(currentInstalling, totalCount);
            installer.Run();
            string fmtString = App.GetPublicResouce<string>("AppInstallingFinishedFmt");
            WriteLine(string.Format(fmtString, successed, error, totalCount));
        }

        private void SetTip(double crt, double total)
        {
            Tip = string.Format(App.GetPublicResouce<string>("AppInstallingFmt"), crt, total);
            Progress = crt / total * 100.0;
        }

        protected override bool VisualStop()
        {
            try
            {
                installer?.ForceStop();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
