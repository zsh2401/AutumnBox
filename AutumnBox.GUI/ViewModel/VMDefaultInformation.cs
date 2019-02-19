/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 17:57:19 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Net;
using AutumnBox.GUI.Util.Net.Getters;
using AutumnBox.GUI.View.DialogContent;
using AutumnBox.GUI.View.Windows;
using AutumnBox.Logging;
using MaterialDesignThemes.Wpf;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.ViewModel
{
    class VMDefaultInformation : ViewModelBase
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

        public VMDefaultInformation()
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
