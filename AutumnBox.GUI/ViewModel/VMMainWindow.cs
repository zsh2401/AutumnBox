/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 19:04:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util;
using AutumnBox.GUI.Util.I18N;
using System;
using System.Windows;

namespace AutumnBox.GUI.ViewModel
{
    class VMMainWindow : ViewModelBase, AppLoader.ILoadingUI
    {
        public string Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
                RaisePropertyChanged();
            }
        }
        private string _version;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }
        private string _title;

        public VMMainWindow()
        {
            LanguageManager.Instance.LanguageChanged += (s, e) =>
            {
                InitTitle();
            };
            InitTitle();
            base.RaisePropertyChangedOnDispatcher = true;
#if PREVIEW
            Version = $"{Self.Version.ToString(3)} {App.Current.Resources["Preview"]}";
#else
            Version = $"{Self.Version.ToString(3)}";
#endif
        }

        private void Instance_LanguageChanged(object sender, EventArgs e)
        {
            InitTitle();
        }

        private void InitTitle()
        {
            string comp = "";
#if PREVIEW
            comp = "PREVIEW";
#elif DEBUG
            comp = "DEBUG";
#else
            comp = "STABLE";
#endif

            Title = $"{App.Current.Resources["AppName"]}-{Self.Version.ToString(3)}-{comp}";
            if (Self.HaveAdminPermission)
            {
                Title += " " + App.Current.Resources["TitleSuffixAdmin"];
            }
        }

        public double Progress
        {
            get => progress; set
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    progress = value;
                    RaisePropertyChanged();
                });
            }
        }
        private double progress = 10;

        public string LoadingTip
        {
            get => loadingTip; set
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    loadingTip = value;
                    RaisePropertyChanged();
                });

            }
        }
        private string loadingTip;

        public int TranSelectIndex
        {
            get => tranIndex; set
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    tranIndex = value;
                    RaisePropertyChanged();
                });
            }
        }
        private int tranIndex = 0;

        public void LoadAsync(Action callback = null)
        {
            new AppLoader(this).LoadAsync(callback);
        }

        public void Finish()
        {
            TranSelectIndex++;
        }
    }
}
