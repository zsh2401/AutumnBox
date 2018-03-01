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

    [Obsolete("已使用Settings代替AutumnBox.GUI.Cfg,此处代码现在仅供参考")]
    [JsonObject(MemberSerialization.OptOut)]
    [ConfigProperty(ConfigFile = "autumnbox.json")]
    public sealed class ConfigDataLayout
    {
        public delegate void ValueSetedHandler();
        public event EventHandler ValueChanged;
        public ValueSetedHandler ValueSetedCallback { private get; set; }

        [JsonProperty("isFirstLaunch")]
        public bool IsFirstLaunch
        {
            get { return _isFirstLaunch; }
            set
            {
                _isFirstLaunch = value;
                ValueChanged?.Invoke(this, new EventArgs());
                ValueSetedCallback?.Invoke();
            }
        }
        private bool _isFirstLaunch = true;

        [JsonProperty("skipVersion")]
        public string SkipVersion
        {
            get { return _skipVersion; }
            set
            {
                _skipVersion = value;
                ValueChanged?.Invoke(this, new EventArgs());
            }
        }
        private string _skipVersion;

        [JsonProperty("langName")]
        public string Lang
        {
            get { return _lang; }
            set
            {
                _lang = value;
                ValueChanged?.Invoke(this, new EventArgs());
            }
        }
        private string _lang = "zh-CN";

        [JsonProperty("backgroundA")]
        public byte BackgroundA
        {
            get { return _backgroundA; }
            set
            {
                _backgroundA = value;
                ValueChanged?.Invoke(this, new EventArgs());
            }
        }
        private byte _backgroundA = 255;

        [JsonProperty("bgARGB")]
        public int[] BackgroundARGB
        {
            get { return _backgroundARGB; }
            set
            {
                _backgroundARGB = value;
                ValueChanged?.Invoke(this, new EventArgs());
            }
        }
        private int[] _backgroundARGB = { 255, 255, 255, 255 };

        [JsonProperty("launchDebugWindowOnNext")]
        public bool ShowDebugWindowOnNextLaunch
        {
            get { return _showDebugWindowOnNextLaunch; }
            set
            {
                _showDebugWindowOnNextLaunch = value;
                ValueChanged?.Invoke(this, new EventArgs());
            }
        }
        public bool _showDebugWindowOnNextLaunch = false;

        public ConfigDataLayout()
        {
            SkipVersion = SystemHelper.CurrentVersion.ToString();
        }
    }
}
