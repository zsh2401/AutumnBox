/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 8:32:48 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("安装APK", "en-us:Install APK")]
    [ExtDesc("可直接向手机安装APK,不过要注意允许USB安装哦!", "en-us:Install apk to device")]
    [ExtIcon("Icons.android.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    internal class EApkInstaller : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = Res("EApkInstallerSelectingTitle");
            fileDialog.Filter = Res("EApkInstallerSelectingFilter");
            fileDialog.Multiselect = true;
            WriteLineAndSetTip(MsgWaitingForUser);
            if (fileDialog.ShowDialog() == true)
            {
                WriteLineAndSetTip(MsgRunning);
                List<FileInfo> files = new List<FileInfo>();
                foreach (string fileName in fileDialog.FileNames)
                {
                    files.Add(new FileInfo(fileName));
                }
                Install(files);
            }
            else
            {
                return ERR_CANCELED_BY_USER;
            }
            return exitCodeOnInstalling;
        }

        private int exitCodeOnInstalling = OK;

        private void Install(List<FileInfo> files)
        {
            WriteInitInfo();
            int successed = 0;
            int error = 0;
            int currentInstalling = 1;
            int totalCount = files.Count;
            SetTip(currentInstalling, totalCount);
            foreach (var file in files)
            {
                try
                {
                    var result = CmdStation
                         .GetAdbCommand(TargetDevice, $"install \"{file.FullName}\"")
                         .To(OutputLogger)
                         .Execute();
                    ThrowIfCanceled();
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
                    Logger.Warn("install apk failed", ex);
                    error++;
                }
                if (currentInstalling < totalCount)
                {
                    currentInstalling++;
                    SetTip(currentInstalling, totalCount);
                }
            };
            string fmtString = Res("EAppInstallerFinishedFmt");
            WriteLine(string.Format(fmtString, successed, error, totalCount));
        }

        private void SetTip(double crt, double total)
        {
            Tip = string.Format(Res("EAppInstallerInstallingFmt"), crt, total);
            Progress = crt / total * 100.0;
        }
    }
}
