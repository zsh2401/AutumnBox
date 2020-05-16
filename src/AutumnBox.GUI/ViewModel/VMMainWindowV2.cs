using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;
using AutumnBox.GUI.Util;

using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;

namespace AutumnBox.GUI.ViewModel
{
    class VMMainWindowV2 : ViewModelBase
    {
        [AutoInject]
        private readonly ILanguageManager languageManager;


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


        public VMMainWindowV2()
        {
            base.RaisePropertyChangedOnUIThread = true;
            InitTitle();
            languageManager.LanguageChanged += (s, e) =>
            {
                InitTitle();
            };
            //AppLoader.Instance.Loaded += (s, e) =>
            //{
            //    SLogger<VMMainWindowV2>.Info("switching");
            //    MainWindowBus.SwitchToMainGrid();
            //    SLogger<VMMainWindowV2>.Info("switched");
            //};
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
        }
    }
}
