/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 17:57:19 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Net;
using AutumnBox.GUI.Util.Net.Getters;
using AutumnBox.GUI.View.DialogContent;
using AutumnBox.GUI.View.Windows;
using AutumnBox.Logging;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        public string MOTDTitle
        {
            get => _motdTitle; set
            {
                _motdTitle = value;
                RaisePropertyChanged();
            }
        }
        private string _motdTitle;

        public string MOTD
        {
            get => _motd; set
            {
                _motd = value;
                RaisePropertyChanged();
            }
        }
        private string _motd;

        public string POTDTitle
        {
            get => _potdTitle; set
            {
                _potdTitle = value;
                RaisePropertyChanged();
            }
        }
        private string _potdTitle;

        public byte[] POTD
        {
            get => _potd; set
            {
                _potd = value;
                RaisePropertyChanged();
            }
        }
        private byte[] _potd;

        public string POTDTarget
        {
            get => _potdTarget; set
            {
                _potdTarget = value;
                RaisePropertyChanged();
            }
        }
        private string _potdTarget;

        public Visibility PotdVisibility
        {
            get => _potdVisi; set
            {
                _potdVisi = value;
                RaisePropertyChanged();
            }
        }
        private Visibility _potdVisi = Visibility.Collapsed;

        public Visibility MotdVisibility
        {
            get => _motdVisi; set
            {
                _motdVisi = value;
                RaisePropertyChanged();
            }
        }
        private Visibility _motdVisi = Visibility.Collapsed;

        public Visibility PotdTitleVisibility
        {
            get => _potdTitleVisi; set
            {
                _potdTitleVisi = value;
                RaisePropertyChanged();
            }
        }
        private Visibility _potdTitleVisi = Visibility.Collapsed;

        public Visibility MotdTitleVisibility
        {
            get => _motdTitleVisi; set
            {
                _motdTitleVisi = value;
                RaisePropertyChanged();
            }
        }
        private Visibility _motdTitleVisi = Visibility.Collapsed;

        public VMHome()
        {
            RaisePropertyChangedOnDispatcher = true;
            Donate = new FlexiableCommand(() =>
            {
                (App.Current.MainWindow as MainWindow).DialogHost.ShowDialog(new ContentDonate());
            });
            ViewOpenSource = new FlexiableCommand(() =>
            {
                (App.Current.MainWindow as MainWindow).DialogHost.ShowDialog(new ContentOpenSource());
            });
            InitPotd();
            InitMotd();
        }

        private void InitPotd()
        {
            new PotdV2Getter().Advance().ContinueWith((task) =>
            {
                if (!task.IsFaulted)
                {
                    POTDTitle = task.Result.Title;
                    POTDTarget = task.Result.ClickTarget;
                    POTD = task.Result.Image;
                    PotdTitleVisibility = task.Result.TitleEnable ? Visibility.Visible : Visibility.Collapsed;
                    PotdVisibility = task.Result.Enable ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                {
                    POTDTitle = "....";
                    POTD = null;
                    PotdVisibility = Visibility.Collapsed;
                    SLogger<VMHome>.Warn("Can not get POTD", task.Exception);
                }
            });
        }

        private void InitMotd()
        {
            new MotdV2Getter().Advance().ContinueWith((task) =>
            {
                if (!task.IsFaulted)
                {
                    MOTDTitle = task.Result.Title;
                    MOTD = task.Result.Content;
                    MotdTitleVisibility = task.Result.TitleEnable ? Visibility.Visible : Visibility.Collapsed;
                    MotdVisibility = task.Result.Enable ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                {
                    MOTDTitle = "...";
                    MOTD = "........";
                    MotdVisibility = Visibility.Collapsed;
                    SLogger<VMHome>.Warn("Can not get MOTD", task.Exception);
                }
            });
        }
    }
}
