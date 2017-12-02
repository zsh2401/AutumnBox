/* =============================================================================*\
*
* Filename: App.xaml.cs
* Description: 
*
* Version: 1.0
* Created: 7/31/2017 05:34:44(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Util;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Windows;
using AutumnBox.Support.CstmDebug;
using System;
using System.Threading;
using System.Windows;

namespace AutumnBox.GUI
{
    [LogProperty(TAG = "AB_App")]
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        internal static DeviceBasicInfo SelectedDevice = new DeviceBasicInfo() { Status = DeviceStatus.None };
        internal static DevicesMonitor DevicesListener = new DevicesMonitor();//设备监听器
        protected override void OnStartup(StartupEventArgs e)
        {
            Logger.T("Init static object start...");
            SelectedDevice = new DeviceBasicInfo() { Status = DeviceStatus.None };
            Logger.T("Init static object finished...");
            Logger.D("Check have other autumnbox process?");
            if (SystemHelper.HaveOtherAutumnBoxProcess())
            {
                Logger.T("have other autumnbox show MMessageBox and exit(1)");
                MMessageBox.FastShow("警告/Warning", "不可以同时打开两个AutumnBox\nDo not run two AutumnBox at once");
                SystemHelper.AppExit(1);
            }
            Logger.D("check finished....start autogc");
            try { SystemHelper.AutoGC.Start(); }
            catch (Exception e_)
            {
                Logger.T("Auto GC start is failed..... -> ", e_);
            }
            Logger.D("Startup!");
            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            Logger.T("App OnExit.... Exit code ->" + e.ApplicationExitCode);
            base.OnExit(e);
            SystemHelper.AppExit(e.ApplicationExitCode);
        }
    }
}
