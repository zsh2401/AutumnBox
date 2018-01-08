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
using AutumnBox.GUI.Helper;
using AutumnBox.Support.CstmDebug;
using System;
using System.IO;
using System.Windows;
namespace AutumnBox.GUI
{

    [LogProperty(TAG = "AB_App")]
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        internal class StaticProperty
        {
            internal static DevicesMonitor DevicesListener { get; private set; }
            static StaticProperty()
            {
                DevicesListener = new DevicesMonitor();//设备监听器
            }
        }
        static App()
        {

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            App.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
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

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(
                $"一个未知的错误的发生了,将logs文件夹压缩并发送给开发者以解决问题{Environment.NewLine}Please compress the logs folder and send it to zsh2401@163.com", 
                "AutumnBox 错误/Unknow Exception",
            MessageBoxButton.OK,
            MessageBoxImage.Warning);
            string n = Environment.NewLine;
            string exstr =
                $"AutumnBox Exception {DateTime.Now.ToString("MM/dd/yyyy    HH:mm:ss")}{n}{n}"+
                $"Exception:{n}{e.Exception.ToString()}{n}{n}{n}" +
                $"Message:{n}{e.Exception.Message}{n}{n}{n}" +
                $"Source:{n}{e.Exception.Source}{n}{n}{n}" +
                $"Inner:{n}{e.Exception.InnerException?.ToString()??"None"}{n}";
            if (!Directory.Exists("logs")) {
                Directory.CreateDirectory("logs");
            }
            File.WriteAllText("logs\\exception.txt", exstr);

#if! DEBUG
            e.Handled = true;
            Environment.Exit(1);
#endif
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Logger.T("App Exiting->" + e.ApplicationExitCode);
            base.OnExit(e);
            SystemHelper.AppExit(e.ApplicationExitCode);
        }
    }
}
