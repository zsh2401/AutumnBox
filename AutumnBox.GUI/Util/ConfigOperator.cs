/* =============================================================================*\
*
* Filename: ConfigJson.cs
* Description: 
*
* Version: 1.0
* Created: 9/30/2017 18:32:35(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.GUI.Helper;
using AutumnBox.Shared;
using AutumnBox.Shared.CstmDebug;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace AutumnBox.GUI.Util
{
    [LogSenderProp(Show = false)]
    public class ConfigOperator : IConfigOperator
    {
        public ConfigTemplate Data { get; private set; } = new ConfigTemplate();
        private static readonly string ConfigFileName = "autumnbox.json";
        public ConfigOperator()
        {
            Logger.D(this, "Start Check");
            if (HaveError() || HaveLost())
            {
                Logger.D(this, "Some error checked, init file");
                SaveToDisk();
            }
            Logger.D(this, "Finished Check");
            ReloadFromDisk();
        }

        public void ReloadFromDisk()
        {
            if (HaveError()) SaveToDisk();
            if (!File.Exists(ConfigFileName)) { SaveToDisk(); return; }
            Data = (ConfigTemplate)(JsonConvert.DeserializeObject(File.ReadAllText(ConfigFileName), Data.GetType()));
            Logger.D(this, "Is first launch? " + Data.IsFirstLaunch.ToString());
        }
        public void SaveToDisk()
        {
            if (!File.Exists(ConfigFileName)) File.Create(ConfigFileName);
            using (StreamWriter sw = new StreamWriter(ConfigFileName, false))
            {
                string text = JsonConvert.SerializeObject(Data);
                Logger.D(this, text);
                sw.Write(text);
                sw.Flush();
            }
        }

        private bool HaveError()
        {
            Logger.D(this, "enter error check");
            try
            {
                JObject jObj = JObject.Parse(File.ReadAllText(ConfigFileName)); return false;
            }
            catch (JsonReaderException) { return true; }
            catch (FileNotFoundException) { return true; }
        }
        private bool HaveLost()
        {
            Logger.D(this, "enter lost check");
            JObject j = JObject.Parse(File.ReadAllText(ConfigFileName));
            Logger.D(this, "read finish");
            foreach (var prop in Data.GetType().GetProperties())
            {
                if (!(prop.IsDefined(typeof(JsonPropertyAttribute)))) continue;
                var attr = (JsonPropertyAttribute)prop.GetCustomAttribute(typeof(JsonPropertyAttribute));
                if (j[attr.PropertyName] == null) { Logger.D(this, "have lost"); return true; };
            }
            Logger.D(this, "no lost");
            return false;
        }
    }
}
