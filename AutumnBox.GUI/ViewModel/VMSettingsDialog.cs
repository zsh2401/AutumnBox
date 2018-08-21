/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/21 20:19:06 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.I18N;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.ViewModel
{
    class VMSettingsDialog : ViewModelBase
    {
        #region MVVM
        public IEnumerable<ILanguage> Languages
        {
            get => _langs;
            set
            {
                _langs = value;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<ILanguage> _langs;

        public ILanguage SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                ApplyLanguage();
                RaisePropertyChanged();
            }
        }
        private ILanguage _selectedItem;

        public string DisplayMemberPath { get; set; } = nameof(ILanguage.LangName);
        #endregion
        public VMSettingsDialog()
        {
            Languages = LanguageManager.Instance.Languages;
            SelectedItem = LanguageManager.Instance.Current;
        }
        private void ApplyLanguage()
        {
            if (SelectedItem.Equals(LanguageManager.Instance.Current))
            {
                return;
            }
            else
            {
                LanguageManager.Instance.Current = SelectedItem;
            }
        }
    }
}
