using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace AutumnBox.OpenFramework.Extension
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class ExtensionI18NTextInfoAttribute : ExtensionInfoAttribute
    {

        public sealed override string Key
        {
            get
            {
                return $"ExtensionTextI18NInfo-{this.GetType().Name}-{Id}";
            }
        }
        public sealed override object Value
        {
            get
            {
                var langCode = Thread.CurrentThread.CurrentCulture.Name;
                if (texts.TryGetValue(langCode, out string text))
                {
                    return text;
                }
                else
                {
                    return defaultText;
                }
            }
        }

        public string Id { get; }

        private readonly string defaultText;
        private readonly IDictionary<string, string> texts;

        public ExtensionI18NTextInfoAttribute(string id,
            string defaultText,
            params (string langCode, string text)[] other)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }
            Id = id;
            this.defaultText = defaultText;
            var dictionary = new Dictionary<string, string>();
            other.All((t) =>
            {
                dictionary.Add(t.langCode, t.text);
                return true;
            });
            texts = new ReadOnlyDictionary<string, string>(dictionary);
        }
    }
}
