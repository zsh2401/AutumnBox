using AutumnBox.Debug;
using AutumnBox.SharedTools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.NetUtil
{
    public class MOTDGetFinishedEventArgs : EventArgs
    {
        public string Header { get; set; }
        public string Message { get; set; }
    }
    public class MOTDGetter
    {
        public void Run(Action<object, MOTDGetFinishedEventArgs> GetFinished)
        {
            try
            {
#if !DEBUG
               JObject o = JObject.Parse(NetHelper.GetHtmlCode(ApiUrl.MOTD));
#else
                JObject o = JObject.Parse(File.ReadAllText(@"E:\zsh2401.github.io\softsupport\autumnbox\motd\index.html"));
#endif
                MOTDGetFinishedEventArgs e = new MOTDGetFinishedEventArgs();
                e.Header = o["header"].ToString();
                e.Message = o["message"].ToString();
                GetFinished(this, e);
            }
            catch (Exception e)
            {
                Basic.Util.Logger.E(this, "Motd Getting Expception", e);
                Log.d("MOTDGetter", e.ToString() + e.Message);
            }
        }
    }
}
