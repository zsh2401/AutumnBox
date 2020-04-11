using System;
using System.Collections.Generic;

namespace AutumnBox.GUI.Services
{

    interface ILanguageManager
    {
        ILanguage Current { get; set; }
        IEnumerable<ILanguage> Languages { get; }
        event EventHandler LanguageChanged;
        void ApplyByLanguageCode(string langCode);
        void ApplyBySetting();
        void ApplyByEnvoriment();
    }
}
