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
using AutumnBox.Shared;
using AutumnBox.Shared.CstmDebug;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;
namespace AutumnBox.GUI.NetUtil
{
    public class MOTDGetFinishedEventArgs : EventArgs
    {
        public string Header { get; set; }
        public string Message { get; set; }
    }
    [LogProperty(TAG = "MOTD Getter")]
    [NetUnitProperty(UseLocalApi = false,MustAddFininshedEventHandler = true)]
    public class MOTDGetter : NetUnitBase,INetUtil
    {
        public event Action<object, MOTDGetFinishedEventArgs> GetFinished;
        public override void Run()
        {
            Logger.T("Start get MOTD");
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
                Logger.T( "Motd Getting Expception", e);
            }
        }
    }
}
