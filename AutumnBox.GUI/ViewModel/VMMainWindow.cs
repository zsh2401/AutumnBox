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
            base.RaisePropertyChangedOnDispatcher = true;
            InitTitle();
            LanguageManager.Instance.LanguageChanged += (s, e) =>
            {
                InitTitle();
            };
#if PREVIEW
            Version = $"{Self.Version.ToString(3)} {App.Current.Resources["VersionTypePreview"]}";
#elif DEBUG
            Version = $"{Self.Version.ToString(3)} {App.Current.Resources["VersionTypeBeta"]}";
#else
            Version = $"{Self.Version.ToString(3)} {App.Current.Resources["VersionTypeStable"]}";
#endif
        }

        private void Instance_LanguageChanged(object sender, EventArgs e)
        {
            InitTitle();
        }

        private void InitTitle()
        {
#if PREVIEW
            Title = $"{App.Current.Resources["AppName"]}-{Self.Version.ToString(3)}-{App.Current.Resources["VersionTypePreview"]}";
#elif DEBUG
           Title = $"{App.Current.Resources["AppName"]}-{Self.Version.ToString(3)}-{{App.Current.Resources["VersionTypeBeta"]}";
#elif RELEASE
           Title = $"{App.Current.Resources["AppName"]}-{Self.Version.ToString(2)}";
#endif

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
