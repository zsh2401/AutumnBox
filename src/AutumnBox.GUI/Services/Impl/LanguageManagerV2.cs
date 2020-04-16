using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Resources.Languages;
using AutumnBox.Leafx.Container.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(ILanguageManager))]
    class LanguageManagerV2 : ILanguageManager
    {
        private const int INDEX_OF_LANG_RES = 0;
        private const string DEFAULT_LANGUAGE_CODE = "en-US";
        private const string DEFAULT_LOADED_LANGUAGE = "zh-CN";

        public event EventHandler LanguageChanged;

        public ILanguage Current
        {
            get => _current; set
            {
                //屏蔽无用操作
                if (value == null && (Current as ILanguage)?.Equals(value) == true)
                {
                    return;
                }
                if (!LoadedLanguages.Contains(value))
                {
                    LoadLanguage(value);
                }
                //设置语言资源
                App.Current.Resources.MergedDictionaries[INDEX_OF_LANG_RES] = value.Resource;
                //保存当前设置
                _current = value;
                //触发事件
                LanguageChanged?.Invoke(this, new EventArgs());
            }
        }
        private ILanguage _current;

        public IEnumerable<ILanguage> LoadedLanguages => languages;
        private readonly List<ILanguage> languages = new List<ILanguage>();

        public ILanguage DefaultLanguage => this.FindLanguageByCode(DEFAULT_LANGUAGE_CODE);

        public LanguageManagerV2()
        {
            var langs = from langInfo in Lang.Langs
                        select new Language(langInfo.Item1, langInfo.Item2, langInfo.Item3);
            languages.AddRange(langs);

            _current = LoadedLanguages.Where(l => l.LanCode == DEFAULT_LOADED_LANGUAGE).FirstOrDefault();
        }


        ~LanguageManagerV2()
        {
            Settings.Default.Language = this.Current.LanCode;
            Settings.Default.Save();
        }

        private class Language : ILanguage, IEquatable<ILanguage>
        {
            public string LanCode { get; }

            public string LangName { get; }

            public ResourceDictionary Resource { get; }

            public Language(string langName, string langCode, string langFileUri)
            {
                if (string.IsNullOrEmpty(langName))
                {
                    throw new ArgumentException("message", nameof(langName));
                }

                if (string.IsNullOrEmpty(langCode))
                {
                    throw new ArgumentException("message", nameof(langCode));
                }

                if (string.IsNullOrEmpty(langFileUri))
                {
                    throw new ArgumentException("message", nameof(langFileUri));
                }

                this.LanCode = langCode;
                this.LangName = langName;
                this.Resource = new ResourceDictionary() { Source = new Uri(langFileUri) };
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as ILanguage);
            }

            public bool Equals(ILanguage other)
            {
                return other != null &&
                       LanCode == other.LanCode &&
                       LangName == other.LangName &&
                       EqualityComparer<ResourceDictionary>.Default.Equals(Resource, other.Resource);
            }

            public override int GetHashCode()
            {
                int hashCode = 1974580400;
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LanCode);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LangName);
                hashCode = hashCode * -1521134295 + EqualityComparer<ResourceDictionary>.Default.GetHashCode(Resource);
                return hashCode;
            }

            public static bool operator ==(Language left, Language right)
            {
                return EqualityComparer<Language>.Default.Equals(left, right);
            }

            public static bool operator !=(Language left, Language right)
            {
                return !(left == right);
            }
        }

        public void LoadLanguage(ILanguage language)
        {
            this.languages.Add(language);
        }
    }
}
