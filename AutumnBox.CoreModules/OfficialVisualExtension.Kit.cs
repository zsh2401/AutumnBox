/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/24 1:46:21 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules
{
    public abstract partial class OfficialVisualExtension : AtmbVisualExtension
    {
        protected string MsgInit => Res("ExtensionIniting");
        protected string MsgWaitingForUser => Res("ExtensionWaitingForUser");
        protected string MsgExitCodeIs => Res("ExtensionExitCodeIs");
        protected string MsgRunning => Res("ExtensionRunning");
        protected string MsgCancelledByUser=> Res("ExtensionCancelByUser");
        protected void WriteLineAndSetTip(string msg)
        {
            WriteLine(msg);
            Tip = msg;
        }
        protected void OutputPrinter(OutputReceivedEventArgs e)
        {
            WriteLine(e.Text);
            Logger.Info(e.Text);
        }
        protected void OutputLogger(OutputReceivedEventArgs e)
        {
            Logger.Info(e.Text);
        }
        protected virtual string Res(string key)
        {
            string result = key;
            App.RunOnUIThread(() =>
            {
                result = App.GetPublicResouce(key)?.ToString() ?? CoreLib.Current.Languages.Get(key) ?? key;
            });
            return result;
        }
        protected void WriteExitCode(int exitCode)
        {
            WriteLine(MsgExitCodeIs + exitCode);
        }
        protected void WriteInitInfo()
        {
            WriteLine(MsgInit);
        }
        protected void WriteWaitingForUser()
        {
            WriteLine(MsgWaitingForUser);
        }
        protected void WriteRunning()
        {
            WriteLine(MsgRunning);
        }
        protected void WriteCommand(IProcessBasedCommand command)
        {
            string msg = string.Format(Res("ExtensionExecutingCommandFmt"), command);
            WriteLine(msg);
        }
    }
}
