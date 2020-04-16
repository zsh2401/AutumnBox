using AutumnBox.GUI.Properties;
using System.Linq;

namespace AutumnBox.GUI.Services
{
    internal static class LanguageManagerExtension
    {
        public static ILanguage FindLanguageByCode(this ILanguageManager languageManager, string langCode)
        {
            return (from lang in languageManager.LoadedLanguages
                    where lang.LanCode == langCode
                    select lang).FirstOrDefault();
        }

        public static void ApplyByEnvoriment(this ILanguageManager languageManager)
        {
            switch (App.Current.Dispatcher.Thread.CurrentCulture.Name)
            {
                case "zh-TW":
                case "zh-CN":
                case "zh-SG":
                case "zh-HK":
                    languageManager.Current = languageManager.FindLanguageByCode("zh-CN");
                    break;
                default:
                    languageManager.Current = languageManager.DefaultLanguage;
                    break;
            }
        }

        public static void ApplyByLanguageCode(this ILanguageManager languageManager, string langCode)
        {
            languageManager.Current = languageManager.FindLanguageByCode(langCode) ?? languageManager.LoadedLanguages.ElementAt(0);
        }

        public static void ApplyBySetting(this ILanguageManager languageManager)
        {
            languageManager.ApplyByLanguageCode(Settings.Default.Language);
        }
    }
}