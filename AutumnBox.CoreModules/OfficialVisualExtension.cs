/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/24 1:46:21 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules
{
    [ExtAuth("秋之盒-zsh2401")]
    [ExtAuth("AutumnBox official", Lang = "en-us")]
    [ExtOfficial(true)]
    [ContextPermission(CtxPer.High)]
    public abstract class OfficialVisualExtension : AtmbVisualExtension
    {
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
