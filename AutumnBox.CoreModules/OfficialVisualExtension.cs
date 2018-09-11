/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/24 1:46:21 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules
{
    [ExtAuth("AutumnBox")]
    [ExtAuth("AutumnBox official", Lang = "en-us")]
    [ExtOfficial(true)]
    [ContextPermission(CtxPer.High)]
    public abstract class OfficialVisualExtension : AtmbVisualExtension
    {
        private class SoundPlayAspect : ExtMainAsceptAttribute
        {
            public override void After(AfterArgs args)
            {
                base.After(args);
                if (args.ReturnCode == 0 && !args.IsForceStopped)
                {
                    args.Extension.SoundPlayer.OK();
                }
            }
        }
        [SoundPlayAspect]
        public override int Main()
        {
            return base.Main();
        }
        protected void WriteLineAndSetTip(string msg)
        {
            WriteLine(msg);
            Tip = msg;
        }
        protected virtual void OutputPrinter(OutputReceivedEventArgs e)
        {
            WriteLine(e.Text);
            Logger.Info(e.Text);
        }
        protected virtual void OutputLogger(OutputReceivedEventArgs e)
        {
            Logger.Info(e.Text);
        }
        protected string Res(string key)
        {
            string result = key;
            App.RunOnUIThread(() =>
            {
                result = App.GetPublicResouce(key)?.ToString() ?? key;
            });
            return result;
        }
        protected override bool VisualStop()
        {
            return false;
        }
    }
}
