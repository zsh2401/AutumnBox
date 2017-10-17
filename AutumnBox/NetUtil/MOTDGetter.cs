/* =============================================================================*\
*
* Filename: MOTDGetter.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 00:58:16(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.SharedTools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;
namespace AutumnBox.NetUtil
{
    public class MOTDGetFinishedEventArgs : EventArgs
    {
        public string Header { get; set; }
        public string Message { get; set; }
    }
    [NetUnitProperty(UseLocalApi = false)]
    public class MOTDGetter : NetUnitBase,INetUnit
    {
        public event Action<object, MOTDGetFinishedEventArgs> GetFinished;
        public override void Run()
        {
            new Hashtable()["fuck"] = null;
            try
            {
                JObject o;
                if (PropertyInfo.UseLocalApi) o = JObject.Parse(NetHelper.GetHtmlCode(Urls.MOTD_API));
                else o = JObject.Parse(File.ReadAllText(@"E:\zsh2401.github.io\softsupport\autumnbox\motd\index.html"));
                MOTDGetFinishedEventArgs e = new MOTDGetFinishedEventArgs
                {
                    Header = o["header"].ToString(),
                    Message = o["message"].ToString()
                };
                GetFinished?.Invoke(this, e);
            }
            catch (Exception e)
            {
                Util.Logger.E(this, "Motd Getting Expception", e);
            }
        }
    }
}
