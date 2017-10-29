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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ConfigTemplate
    {
        [JsonProperty("IsFirstLaunch")]
        public bool IsFirstLaunch { get; set; } = true;
        [JsonProperty("SkipVersion")]
        public string SkipVersion { get; set; } = "0.0.0.0";
        [JsonProperty("Lang")]
        public string Lang { get; set; } = "zh-CN";
    }
}
