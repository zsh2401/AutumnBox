using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.I18N;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.ViewModel
{
    class VMLanguageChoice : ViewModelBase
    {
        public IEnumerable<ILanguage> Languages => LanguageManager.Instance.Languages;
        public ILanguage SelectedLanguage
        {
            get => LanguageManager.Instance.Current;
            set
            {
                LanguageManager.Instance.Current = value;
                RaisePropertyChanged();
            }
        }
    }
}
