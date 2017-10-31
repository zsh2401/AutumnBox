/* =============================================================================*\
*
* Filename: ConfigTemplate
* Description: 
*
* Version: 1.0
* Created: 2017/10/29 21:29:48 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.GUI.Helper;
using Newtonsoft.Json;
using System;

namespace AutumnBox.GUI.Cfg
{
    [JsonObject(MemberSerialization.OptOut)]
    [ConfigProperty(ConfigFile = "autumnbox.json")]
    internal sealed class ConfigDataLayout
    {
        [JsonProperty("isFirstLaunch")]
        public bool IsFirstLaunch { get; set; } = true;
        [JsonProperty("skipVersion")]
        public string SkipVersion { get; set; }
        [JsonProperty("langName")]
        public string Lang { get; set; } = "zh-CN";
        [JsonProperty("backgroundA")]
        public byte BackgroundA { get; set; } = 255;
        [JsonProperty("bgARGB")]
        public int[] BackgroundARGB { get; set; } = { 255, 255, 255, 255 };
        public ConfigDataLayout()
        {
            SkipVersion = SystemHelper.CurrentVersion.ToString();
        }
    }
}
