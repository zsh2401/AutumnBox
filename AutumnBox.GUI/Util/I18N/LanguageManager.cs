/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 15:11:15 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.Util.I18N
{
    /// <summary>
    /// 语言管理器
    /// </summary>
    class LanguageManager : ILanguageManager
    {
        private const int INDEX_OF_LANG = 0;
        private const string FILE_PATH = "pack://application:,,,/AutumnBox.GUI;component/Resources/Languages/";
        private const string LANG_NAME_KEY = "LanguageName";
        private const string LANG_CODE_KEY = "LanguageCode";
        private const string DEFAULT_LANG_CODE = "en-US";
        public static LanguageManager Instance { get; private set; }
        static LanguageManager()
        {
            Instance = new LanguageManager();
        }

        public ILanguage Current
        {
            get
            {
                return current;
            }
            set
            {
                Apply(value);
            }
        }
        private ILanguage current;

        public IEnumerable<ILanguage> Languages => languages;
        private List<Language> languages;

        public event EventHandler LanguageChanged;

        private LanguageManager()
        {
            AllLoad();
            Instance = this;
        }
        private void AllLoad()
        {
            languages = new List<Language>
            {
                Language.From("zh-CN.xaml"),
                Language.From("en-US.xaml"),
            };
        }
        private void Apply(ILanguage lang)
        {
            App.Current.Resources.MergedDictionaries[INDEX_OF_LANG] = lang.Resource;
            current = lang;
            Settings.Default.Language = lang.LanCode;
            Settings.Default.Save();
            LanguageChanged?.Invoke(this, new EventArgs());
        }
        public void ApplyBySetting()
        {
            ApplyByLanguageCode(Settings.Default.Language);
        }
        public void ApplyByEnvoriment()
        {
            switch (System.Threading.Thread.CurrentThread.CurrentCulture.Name)
            {
                case "zh-TW":
                case "zh-CN":
                case "zh-SG":
                case "zh-HK":
                    ApplyByLanguageCode("zh-CN");
                    break;
                default:
                    ApplyByLanguageCode(DEFAULT_LANG_CODE);
                    break;
            }
        }
        public void ApplyByLanguageCode(string langCode)
        {
            var lang = languages.Find((_lang) => _lang.LanCode == langCode);
            if (lang != null)
            {
                Apply(lang);
            }
            else
            {
                ApplyByLanguageCode(DEFAULT_LANG_CODE);
            }
        }
        private class Language : ILanguage
        {
            public string LangName => Resource[LANG_NAME_KEY].ToString();
            public string LanCode => Resource[LANG_CODE_KEY].ToString();
            public ResourceDictionary Resource { get; private set; }
            private Language() { }
            public static Language From(string fileName)
            {
                var resouceDict = new ResourceDictionary { Source = new Uri(FILE_PATH + fileName) };
                return new Language()
                {
                    Resource = resouceDict,
                };
            }
        }
    }
}
