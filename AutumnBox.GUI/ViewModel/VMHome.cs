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

        public ICommand Refresh
        {
            get => _refresh; set
            {
                _refresh = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _refresh;

        public IEnumerable<Tip> Tips
        {
            get => _tips; set
            {
                _tips = value;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<Tip> _tips;

        public object CstXamlObject
        {
            get => _cstXamlObj; set
            {
                _cstXamlObj = value;
                RaisePropertyChanged();
            }
        }
        private object _cstXamlObj;

        private ParserContext Context { get; set; }

        public VMHome()
        {
            RaisePropertyChangedOnDispatcher = true;
            InitParserContext();
            Donate = new FlexiableCommand(() =>
            {
                (App.Current.MainWindow as MainWindow).DialogHost.ShowDialog(new ContentDonate());
            });
            ViewOpenSource = new FlexiableCommand(() =>
            {
                (App.Current.MainWindow as MainWindow).DialogHost.ShowDialog(new ContentOpenSource());
            });
            Refresh = new FlexiableCommand(() =>
            {
                _RefreshTips();
                _RefreshCstXaml();
            });
            _RefreshTips();
            _RefreshCstXaml();
        }

        private void InitParserContext()
        {
            Context = new ParserContext();
            Context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            Context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            Context.XmlnsDictionary.Add("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            Context.XmlnsDictionary.Add("materialDesign", "http://materialdesigninxaml.net/winfx/xaml/themes");
        }

        private void _RefreshTips()
        {
            Tips = null;
            new TipsGetter().Advance().ContinueWith(task =>
            {
                SLogger<VMHome>.Info("finished");
                if (task.IsFaulted)
                {
                    Tips = new List<Tip>();
                    SLogger<VMHome>.Warn("Can not refresh tips", task.Exception);
                }
                else
                {
                    Tips = task.Result.Tips;
                }
            });
        }

        private void _RefreshCstXaml()
        {
            CstXamlObject = null;
            new CstGetter().DoAsync(Context).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    SLogger<VMHome>.Warn("Can not parse CstXaml", task.Exception);
                }
                else
                {
                    CstXamlObject = task.Result;
                }
            });
        }
    }
}
