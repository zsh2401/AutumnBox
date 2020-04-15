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
        public ILanguage Current
        {
            get => _current; set
            {
                //屏蔽无用操作
                if (value == null || value.GetHashCode() == Current.GetHashCode())
                {
                    return;
                }
                App.Current.Resources.MergedDictionaries[INDEX_OF_LANG_RES] = _current.Resource;
                _current = value;
                LanguageChanged?.Invoke(this, new EventArgs());
            }
        }
        private ILanguage _current = null;

        public IEnumerable<ILanguage> Languages { get; }

        public LanguageManagerV2()
        {
            Languages = from langInfo in Lang.Langs
                        orderby langInfo.Item2 == DEFAULT_LANGUAGE_CODE descending
                        select new Language(langInfo.Item1, langInfo.Item2, langInfo.Item3);

            _current = Languages.Where(l => l.LanCode == DEFAULT_LOADED_LANGUAGE).FirstOrDefault();
        }

        public event EventHandler LanguageChanged;

        public void ApplyByEnvoriment()
        {
            switch (Thread.CurrentThread.CurrentCulture.Name)
            {
                case "zh-TW":
                case "zh-CN":
                case "zh-SG":
                case "zh-HK":
                    ApplyByLanguageCode("zh-CN");
                    break;
                default:
                    ApplyByLanguageCode(DEFAULT_LANGUAGE_CODE);
                    break;
            }
        }

        public void ApplyByLanguageCode(string langCode)
        {
            var result = Languages.Where(lang => lang.LanCode == langCode).FirstOrDefault();
            if (result == null) result = Languages.Where(lang => lang.LanCode == DEFAULT_LANGUAGE_CODE).FirstOrDefault();
            Current = result;
        }

        public void ApplyBySetting()
        {
            ApplyByLanguageCode(Settings.Default.Language);
        }

        ~LanguageManagerV2()
        {
            Settings.Default.Language = this.Current.LanCode;
            Settings.Default.Save();
        }

        private class Language : ILanguage, IEquatable<Language>
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

            public bool Equals(ILanguage other)
            {
                return other != null &&
       LanCode == other.LanCode &&
       LangName == other.LangName &&
       EqualityComparer<ResourceDictionary>.Default.Equals(Resource, other.Resource);
            }

            public bool Equals(Language other)
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

            public override bool Equals(object obj)
            {
                return this.Equals(obj);
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
    }
}
