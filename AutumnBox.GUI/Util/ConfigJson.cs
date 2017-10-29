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

    [LogSenderProp(TAG = "Config Object")]
    [JsonObject(MemberSerialization.OptOut)]
    public class ConfigJson
    {
        [JsonProperty("IsFirstLaunch")]
        public bool IsFirstLaunch { get; set; } = true;
        [JsonProperty("SkipVersion")]
        public string SkipVersion { get; set; } = "0.0.0";
        [JsonProperty("Lang")]
        public string Lang { get; set; } = "zh-CN";

        private class JObjectGetter
        {
            private JObject Source;
            private ConfigJson Owner;
            public JObjectGetter(ConfigJson owner, String jsontext)
            {
                Source = JObject.Parse(jsontext);
                Owner = owner;
            }
            public JObjectGetter(ConfigJson owner, JObject jObject)
            {
                Source = jObject;
                Owner = owner;
            }
            public JToken Get(string propName)
            {
                return Source[DataHelper.JsonPropertyNameOf(Owner, propName)] ?? throw new NullReferenceException();
            }
        }
        private static readonly string ConfigFileName = "autumnbox.json";
        public ConfigJson()
        {
            Logger.D(this, "Start Check");
            if (HaveError() || HaveLost())
            {
                SaveToDisk();
            }
            Logger.D(this, "Finished Check");
            ReloadFromDisk();
        }

        public void ReloadFromDisk()
        {
            if (HaveError()) SaveToDisk();
            if (!File.Exists(ConfigFileName)) { SaveToDisk(); return; }
            JObjectGetter getter = new JObjectGetter(this, File.ReadAllText(ConfigFileName));
            Logger.D(this, "getter inited");
            IsFirstLaunch = (bool)getter.Get(nameof(IsFirstLaunch));
            Logger.D(this, "ifl finished");
            SkipVersion = (string)getter.Get(nameof(SkipVersion));
            Logger.D(this, "sv finish");
            Lang = (string)getter.Get(nameof(Lang));
            Logger.D(this, "lang finished");
        }
        public void SaveToDisk()
        {
            if (!File.Exists(ConfigFileName)) File.Create(ConfigFileName);
            using (StreamWriter sw = new StreamWriter(ConfigFileName, false))
            {
                sw.Write(JsonConvert.SerializeObject(this));
                sw.Flush();
            }
        }

        public bool HaveError()
        {
            Logger.D(this, "enter error check");
            try
            {
                JObject jObj = JObject.Parse(File.ReadAllText(ConfigFileName)); return false;
            }
            catch (JsonReaderException) { return true; }
            catch (FileNotFoundException) { return true; }
        }
        public bool HaveLost()
        {
            Logger.D(this, "enter lost check");
            JObject j = JObject.Parse(File.ReadAllText(ConfigFileName));
            Logger.D(this, "read finish");
            foreach (var prop in this.GetType().GetProperties())
            {
                if (!(prop.IsDefined(typeof(JsonPropertyAttribute)))) continue;
                var attr = (JsonPropertyAttribute)prop.GetCustomAttribute(typeof(JsonPropertyAttribute));
                if (j[attr.PropertyName] == null) return true;
            }
            return false;
        }
    }
}
