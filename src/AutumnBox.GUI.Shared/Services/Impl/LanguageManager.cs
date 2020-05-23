using AutumnBox.GUI.Resources.Languages;
using AutumnBox.GUI.Util;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Leafx.ObjectManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(ILanguageManager))]
    class LanguageManager : ILanguageManager
    {
        const int INDEX_OF_LANG_RES = 1;
        const string DEFAULT_LANGUAGE_CODE = "en-US";
        const string DEFAULT_LOADED_LANGUAGE = "zh-CN";

        public event EventHandler LanguageChanged;

        public ILanguage Current
        {
            get => _current; set
            {
                //屏蔽无用操作
                if (value == null && Current?.Equals(value) == true)
                {
                    return;
                }
                if (!Languages.Contains(value))
                {
                    Languages.Add(value);
                }
                //设置语言资源
                App.Current.Resources.MergedDictionaries[INDEX_OF_LANG_RES] = value.Resource;

                //保存当前设置
                _current = value;
                Settings.LanguageCode = this.Current.Code;

                //触发事件
                LanguageChanged?.Invoke(this, new EventArgs());
            }
        }
        ILanguage _current;

        [AutoInject] ISettings Settings { get; set; }

        public IList<ILanguage> Languages { get; } = new List<ILanguage>();

        public ILanguage DefaultLanguage => this.FindLanguageByCode(DEFAULT_LANGUAGE_CODE);

        public LanguageManager()
        {
            var langs = from langInfo in Lang.Langs
                        select new Language(langInfo.name, langInfo.code, langInfo.resourceUri);

            langs.All((lang) =>
            {
                Languages.Add(lang);
                return true;
            });

            _current = Languages.Where(l => l.Code == DEFAULT_LOADED_LANGUAGE).FirstOrDefault();
        }

        private class Language : ILanguage, IEquatable<ILanguage>
        {
            public string Code { get; }

            public string Name { get; }

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

                this.Code = langCode;
                this.Name = langName;
                this.Resource = new ResourceDictionary() { Source = new Uri(langFileUri) };
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as ILanguage);
            }

            public bool Equals(ILanguage other)
            {
                return other != null &&
                       Code == other.Code &&
                       Name == other.Name &&
                       EqualityComparer<ResourceDictionary>.Default.Equals(Resource, other.Resource);
            }

            public override int GetHashCode()
            {
                int hashCode = 1974580400;
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Code);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
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
    }
}
