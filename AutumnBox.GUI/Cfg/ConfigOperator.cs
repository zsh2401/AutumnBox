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
using AutumnBox.Support.CstmDebug;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace AutumnBox.GUI.Cfg
{
    [LogProperty(Show = false)]
    internal sealed class ConfigOperator : IConfigOperator
    {
        public ConfigDataLayout Data { get; private set; } = new ConfigDataLayout();
        private readonly string ConfigFileName;
        public ConfigOperator()
        {
            ConfigFileName = ((ConfigPropertyAttribute)Data.GetType().GetCustomAttribute(typeof(ConfigPropertyAttribute))).ConfigFile;
            Logger.D("Start Check");
            if (HaveError() || HaveLost())
            {
                Logger.D("Some error checked, init file");
                SaveToDisk();
            }
            Logger.D("Finished Check");
            try
            {
                ReloadFromDisk();
            }
            catch (Exception)
            {
                SaveToDisk();
                ReloadFromDisk();
            }
        }
        /// <summary>
        /// 从硬盘重载数据__
        /// </summary>
        public void ReloadFromDisk()
        {
            if (HaveError()) SaveToDisk();
            if (!File.Exists(ConfigFileName)) { SaveToDisk(); return; }
            using (StreamReader sr = new StreamReader(ConfigFileName))
            {
                Data = (ConfigDataLayout)(JsonConvert.DeserializeObject(sr.ReadToEnd(), Data.GetType()));
            }
            Data.ValueChanged += (s, e) => { SaveToDisk(); };
            Logger.D("a is???" + Data.BackgroundA);
        }
        /// <summary>
        /// 将数据存储到硬盘
        /// </summary>
        public void SaveToDisk()
        {

            if (!File.Exists(ConfigFileName))
            {
                using (FileStream fs = new FileStream(ConfigFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite)) { }
            }
            using (StreamWriter sw = new StreamWriter(ConfigFileName, false))
            {
                string text = JsonConvert.SerializeObject(Data);
                Logger.D(text);
                sw.Write(text);
                sw.Flush();
            }
        }

        /// <summary>
        /// 检测硬盘上的数据是否有问题
        /// </summary>
        /// <returns>是否有问题</returns>
        private bool HaveError()
        {
            Logger.D("enter error check");
            try
            {
                JObject jObj = JObject.Parse(File.ReadAllText(ConfigFileName)); return false;
            }
            catch (JsonReaderException) { return true; }
            catch (FileNotFoundException) { return true; }
        }
        /// <summary>
        /// 检测配置文件中的项是否有丢失
        /// </summary>
        /// <returns>项是否有丢失</returns>
        private bool HaveLost()
        {
            Logger.D("enter lost check");
            JObject j = JObject.Parse(File.ReadAllText(ConfigFileName));
            Logger.D("read finish");
            foreach (var prop in Data.GetType().GetProperties())
            {
                if (!(prop.IsDefined(typeof(JsonPropertyAttribute)))) continue;
                var attr = (JsonPropertyAttribute)prop.GetCustomAttribute(typeof(JsonPropertyAttribute));
                if (j[attr.PropertyName] == null) { Logger.D("have lost"); return true; };
            }
            Logger.D("no lost");
            return false;
        }
    }
}
