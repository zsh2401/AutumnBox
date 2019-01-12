/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 17:57:19 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.View.DialogContent;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;

namespace AutumnBox.GUI.ViewModel
{
    class VMHome : ViewModelBase
    {
        public FlexiableCommand Donate
        {
            get
            {
                return _donate;
            }
            set
            {
                _donate = value;
                RaisePropertyChanged();
            }
        }
        private FlexiableCommand _donate;
        public VMHome()
        {
            Donate = new FlexiableCommand(() =>
            {
                DialogHost.Show(new ContentDonate());
            });
        }
    }
}
