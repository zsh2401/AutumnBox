/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 19:04:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;
using AutumnBox.GUI.Util;
using AutumnBox.GUI.Util.UI;
using AutumnBox.Leafx.ObjectManagement;
using System.Windows;

namespace AutumnBox.GUI.ViewModel
{
    class VMMainWindow : ViewModelBase
    {
        public string Sentence
        {
            get => _sentence; set
            {
                _sentence = value;
                RaisePropertyChanged();
            }
        }
        private string _sentence;


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

        [AutoInject]
        private readonly ISentenceService sentenceService;

        [AutoInject]
        private readonly ILanguageManager languageManager;

        [AutoInject]
        private readonly IAppLifecycleManager appLifecycleManager;

        public VMMainWindow()
        {
            base.RaisePropertyChangedOnDispatcher = true;
            Sentence = sentenceService.Next();
            InitTitle();
            languageManager.LanguageChanged += (s, e) =>
            {
                InitTitle();
            };
            appLifecycleManager.AppLoaded += (s, e) =>
            {
                TranSelectIndex++;
            };
        }

        private void InitTitle()
        {
#if PREVIEW
            Title = $"{App.Current.Resources["AppName"]}-{Self.Version.ToString(3)}-{App.Current.Resources["VersionTypePreview"]}";
#elif RELEASE
            Title = $"{App.Current.Resources["AppName"]}-{Self.Version.ToString(3)}-{App.Current.Resources["VersionTypeStable"]}";
#else
            Title = $"{App.Current.Resources["AppName"]}-{Self.Version.ToString(3)}-{App.Current.Resources["VersionTypeBeta"]}";
#endif
            if (Self.HaveAdminPermission)
            {
                Title += " " + App.Current.Resources["TitleSuffixAdmin"];
            }
        }

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

        public ResizeMode ResizeMode
        {
            get => _resizeMode; set
            {
                _resizeMode = value;
                RaisePropertyChanged();
            }
        }
        private ResizeMode _resizeMode = ResizeMode.NoResize;
    }
}
