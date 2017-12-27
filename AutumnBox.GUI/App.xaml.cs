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
using AutumnBox.Basic.Connection;
using AutumnBox.Basic.Devices;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Windows;
using AutumnBox.Support.CstmDebug;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace AutumnBox.GUI
{

    //[LogProperty(TAG = "AB_App")]
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        internal class StaticProperty
        {
            internal static DeviceConnection DeviceConnection { get; private set; }
            internal static DevicesMonitor DevicesListener { get; private set; }
            static StaticProperty()
            {
                Debug.WriteLine("first?");
                DeviceConnection = new DeviceConnection();
                DevicesListener = new DevicesMonitor();//设备监听器
            }
        }
        static App()
        {
            //AppDomain.CurrentDomain.AssemblyResolve += (s, args) =>
            //{
            //    string resName = "AutumnBox.GUI.Resources.lib." + args.Name.Split(',')[0] + ".dll";
            //    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resName))
            //    {
            //        byte[] assemblyData = new byte[stream.Length];
            //        stream.Read(assemblyData, 0, assemblyData.Length);
            //        return Assembly.Load(assemblyData);
            //    }
            //};
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            if (SystemHelper.HaveOtherAutumnBoxProcess)
            {
                Logger.T("have other autumnbox show MMessageBox and exit(1)");
                MessageBox.Show("不可以同时打开两个AutumnBox\nDo not run two AutumnBox at once", "警告/Warning");
                SystemHelper.AppExit(1);
            }
            try { SystemHelper.AutoGC.Start(); }
            catch (Exception e_)
            {
                Logger.T("Auto GC failed...... -> ", e_);
            }
            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            Logger.T("App Exiting->" + e.ApplicationExitCode);
            base.OnExit(e);
            SystemHelper.AppExit(e.ApplicationExitCode);
        }
    }
}
