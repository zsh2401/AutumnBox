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
using AutumnBox.Support.CstmDebug;
using Newtonsoft.Json;
using System;

namespace AutumnBox.GUI.Cfg
{
    [JsonObject(MemberSerialization.OptOut)]
    [ConfigProperty(ConfigFile = "autumnbox.json")]
    internal sealed class ConfigDataLayout
    {
        public event EventHandler ValueChanged;
        [JsonProperty("isFirstLaunch")]
        public bool IsFirstLaunch
        {
            get { return _isFirstLaunch; }
            set
            {
                ValueChanged?.Invoke(this, new EventArgs());
                _isFirstLaunch = value;
            }
        }
        private bool _isFirstLaunch = true;
        [JsonProperty("skipVersion")]
        public string SkipVersion
        {
            get { return _skipVersion; }
            set
            {
                ValueChanged?.Invoke(this, new EventArgs());
                _skipVersion = value;
            }
        }
        private string _skipVersion;
        [JsonProperty("langName")]
        public string Lang { get { return _lang; } set { ValueChanged?.Invoke(this, new EventArgs()); _lang = value; } }
        private string _lang = "zh-CN";
        [JsonProperty("backgroundA")]
        public byte BackgroundA
        {
            get { return _backgroundA; }
            set
            {
                ValueChanged?.Invoke(this, new EventArgs());
                _backgroundA = value;
            }
        }
        private byte _backgroundA = 225;
        [JsonProperty("bgARGB")]
        public int[] BackgroundARGB
        {
            get { return _backgroundARGB; }
            set
            {
                ValueChanged?.Invoke(this, new EventArgs());
                _backgroundARGB = value;
            }
        }
        private int[] _backgroundARGB = { 255, 255, 255, 255 };
        public ConfigDataLayout()
        {
            SkipVersion = SystemHelper.CurrentVersion.ToString();
        }
    }
}
