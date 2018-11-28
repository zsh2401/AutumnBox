/* =============================================================================*\
*
* Filename: UpdateChecker.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 00:58:05(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.GUI.Util.Debugging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace AutumnBox.GUI.Util.Net
{

    internal class RemoteVersionInfoGetter : JsonGetter<RemoteVersionInfoGetter.Result>
    {
        [JsonObject(MemberSerialization.OptOut)]
        internal class Result
        {
            [JsonProperty("header")]
            public string Header { get; set; } = "NULL TITLE";
            [JsonProperty("version")]
            public string VersionString { get; set; } = "0.0.0";
            [JsonProperty("message")]
            public string Message { get; set; } = "No update";
            [JsonProperty("updateUrl")]
            public string UpdateUrl { get; set; } = "http://www.atmb.top/";
            [JsonProperty("date")]
            public int[] TimeArray { get; set; } = new int[] { 1970, 1, 1 };

            public Version Version
            {
                get
                {
                    try
                    {
                        return new Version(VersionString);
                    }
                    catch (Exception ex)
                    {
                        SLogger.Warn(this, "Parse VersionString failed", ex);
                        return new Version("0.0.5");
                    }
                }
            }

            public DateTime Time
            {
                get
                {
                    try
                    {
                        return new DateTime(TimeArray[0], TimeArray[1], TimeArray[2]);
                    }
                    catch (Exception ex)
                    {
                        SLogger.Warn(this, "Parse datetime failed", ex);
                        return new DateTime(1970, 1, 1);
                    }
                }
            }
        }
        public RemoteVersionInfoGetter()
        {
            DebugUrl = "http://localhost:24010/_api_/update/";
            Url = App.Current.Resources["urlApiUpdate"].ToString();
        }
    }
}
