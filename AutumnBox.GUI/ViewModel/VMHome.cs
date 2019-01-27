/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 17:57:19 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.View.DialogContent;
using AutumnBox.GUI.View.Windows;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using System.Windows.Input;

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

        public ICommand ViewOpenSource
        {
            get
            {
                return _os;
            }
            set
            {
                _os = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _os;

        public VMHome()
        {
            Donate = new FlexiableCommand(() =>
            {
                (App.Current.MainWindow as MainWindow).DialogHost.ShowDialog(new ContentDonate());
            });
            ViewOpenSource = new FlexiableCommand(() =>
            {
                (App.Current.MainWindow as MainWindow).DialogHost.ShowDialog(new ContentOpenSource());
            });
        }
    }
}
