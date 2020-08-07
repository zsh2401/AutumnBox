/*

* ==============================================================================
*
* Filename: SettingsImpl
* Description: 
*
* Version: 1.0
* Created: 2020/5/20 21:54:23
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.GUI.MVVM;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Logging;
using System;
using System.IO;
#if USE_NT_JSON
using Newtonsoft.Json;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;
#endif

namespace AutumnBox.GUI.Services.Impl
{
#if USE_SYS_JSON
    class JsonIgnoreAttribute : Attribute { }
#endif
    [Component(Type = typeof(ISettings))]
    class SettingsImpl : NotificationObject, ISettings
    {
        public string LanguageCode
        {
            get => _languageCode; set
            {
                _languageCode = value;
                RaisePropertyChanged();
            }
        }
        [JsonIgnore] string _languageCode = "zh-CN";

        public ThemeMode Theme
        {
            get => _theme; set
            {
                _theme = value;
                RaisePropertyChanged();
            }
        }
        [JsonIgnore] ThemeMode _theme = ThemeMode.Auto;

        public bool DeveloperMode
        {
            get => _devMode; set
            {
                _devMode = value;
                RaisePropertyChanged();
            }
        }
        [JsonIgnore] bool _devMode = false;

        public bool ShowDebugWindowNextLaunch
        {
            get => _showDebugWindowNextLaunch; set
            {
                _showDebugWindowNextLaunch = value;
                RaisePropertyChanged();
            }
        }
        [JsonIgnore] bool _showDebugWindowNextLaunch;

        public bool StartCmdAtDesktop
        {
            get => _startCmdAtDesktop; set
            {
                _startCmdAtDesktop = value;
                RaisePropertyChanged();
                if (value)
                {
                    EnvVarCmdWindow = true;
                }
            }
        }
        [JsonIgnore] bool _startCmdAtDesktop = true;

        public bool EnvVarCmdWindow
        {
            get => _envVarCmd; set
            {
                _envVarCmd = value;
                RaisePropertyChanged();
            }
        }
        [JsonIgnore] bool _envVarCmd = true;

        public bool GuidePassed
        {
            get => _guidePassed; set
            {
                _guidePassed = value;
                RaisePropertyChanged();
            }
        }
        [JsonIgnore] bool _guidePassed = false;

        public bool SoundEffect
        {
            get => _soundEffect; set
            {
                _soundEffect = value;
                RaisePropertyChanged();
            }
        }
        [JsonIgnore] bool _soundEffect = true;

        [JsonIgnore] readonly FileInfo settingsFile;

        public SettingsImpl(IStorageManager storageManager)
        {
            var filePath = Path.Combine(storageManager.StorageDirectory.FullName, "settings.json");
            settingsFile = new FileInfo(filePath);
            Load();
        }

        private void Load()
        {
            try
            {
                using var fs = settingsFile.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite);
                using var sr = new StreamReader(fs);
#if USE_NT_JSON
                JsonConvert.PopulateObject(sr.ReadToEnd(), this);
#elif USE_SYS_JSON
                PopulateObject(this, sr.ReadToEnd());
#endif
            }
            catch (Exception)
            {
                //Use default settings;
            }
        }
#if USE_SYS_JSON
        void PopulateObject<T>(T target, string jsonSource) where T : class
        {
            var json = System.Text.Json.JsonDocument.Parse(jsonSource).RootElement;

            foreach (var property in json.EnumerateObject())
            {
                OverwriteProperty(target, property);
            }
        }

        void OverwriteProperty<T>(T target, System.Text.Json.JsonProperty updatedProperty) where T : class
        {
            var propertyInfo = typeof(T).GetProperty(updatedProperty.Name);

            if (propertyInfo == null)
            {
                return;
            }

            var propertyType = propertyInfo.PropertyType;
            var parsedValue = System.Text.Json.JsonSerializer.Deserialize(
                updatedProperty.Value.GetRawText(),
                propertyType);

            propertyInfo.SetValue(target, parsedValue);
        }
#endif
        ~SettingsImpl()
        {
            Save();
        }

        [JsonIgnore]
        readonly object _saveLock = new object();
        public void Save()
        {
            lock (_saveLock)
            {
                using var fs = new FileStream(settingsFile.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                //clear content
                fs.SetLength(0);
                fs.Flush();

                using var sw = new StreamWriter(fs);
                sw.Write(Serialize());
            }
        }
        private string Serialize()
        {
#if USE_NT_JSON
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
#elif USE_SYS_JSON
            return System.Text.Json.JsonSerializer.Serialize(this);
#endif
        }
    }
}
