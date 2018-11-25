/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:53:12 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace AutumnBox.CoreModules.Extensions.Mix
{
    [ExtName("推送文件到手机主目录", "en-us:Push file to device")]
    [ExtIcon("Icons.filepush.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron | Basic.Device.DeviceState.Recovery)]
    internal class EFilePusher : OfficialVisualExtension
    {
        private string _latestLine = null;
        protected override void OutputPrinter(OutputReceivedEventArgs e)
        {
            _latestLine = e.Text;
            //base.OutputPrinter(e);
            //ParseAndShowOnUI(e.Text);
        }
        private bool _cancelReadingLoop = false;
        private void ReadingLoop()
        {
            while (!_cancelReadingLoop)
            {
                Logger.Info(_latestLine);
                WriteLine(_latestLine);
                ParseAndShowOnUI(_latestLine);
                Thread.Sleep(1000);
            }
        }
        static readonly Regex rg12 = new Regex("\\ (.*?)\\%");
        static readonly Regex rg3 = new Regex("\\[(.*?)\\%");
        private void ParseAndShowOnUI(string msg)
        {
            Match m;
            try
            {
                m = rg12.Match(msg);
                if (!m.Success)
                {
                    m = rg3.Match(msg);
                }
                var r = m.Result("$1");
                App.RunOnUIThread(() =>
                {
                    Progress = double.Parse(r);
                    Tip = r.ToString() + "%";
                });
            }
            catch (Exception)
            {
                //Logger.Warn(this, "parse progress text failed", se);
            }
        }
        protected override int VisualMain()
        {
            new Thread(ReadingLoop).Start();
            bool? dialogResult = null;
            string seleFile = null;
            App.RunOnUIThread(() =>
            {
                Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
                fileDialog.Reset();
                fileDialog.Title = Res("EFilePusherSelectingTitle");
                fileDialog.Filter = Res("EFilePusherSelectingFilter");
                fileDialog.Multiselect = false;
                dialogResult = fileDialog.ShowDialog();
                seleFile = fileDialog.FileName;
            });

            if (dialogResult != true) return ERR_CANCELED_BY_USER;
            try
            {
                return GetDeviceAdbCommand($"push \"{seleFile}\" /sdcard/")
                    .To(OutputPrinter)
                    .Execute()
                    .ExitCode;
            }
            catch (Exception ex)
            {
                Logger.Warn("file pushing failed", ex);
                WriteLineAndSetTip(Res("EFilePusherFailed"));
                //FinishedTip = "EFilePusherFailed";
                return ERR;
            }
        }
        protected override void OnFinish(FinishedArgs args)
        {
            base.OnFinish(args);
            _cancelReadingLoop = true;
        }
    }
}
