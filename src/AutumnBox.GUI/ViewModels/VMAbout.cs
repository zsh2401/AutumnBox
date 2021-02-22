using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.ObjectManagement;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModels
{
    class VMAbout : ViewModelBase
    {
        public ICommand UpdateCheck { get; }

        public string VersionName
        {
            get => _versionName; set
            {
                _versionName = value;
                RaisePropertyChanged();
            }
        }
        private string _versionName;

        public string Tag
        {
            get => _tag; set
            {
                _tag = value;
                RaisePropertyChanged();
            }
        }
        private string _tag;

        public string Commit
        {
            get => _commit;
            set
            {
                _commit = value;
                RaisePropertyChanged();
            }
        }
        private string _commit;

        [AutoInject]
        private IOpenFxManager OpenFxManager { get; set; }

        public VMAbout()
        {
            UpdateCheck = new MVVMCommand((p) => OpenFxManager.RunExtension("EAutumnBoxUpdateChecker"));
            var buildInfo = App.Current.Lake.Get<IBuildInfo>();
            VersionName = buildInfo.Version;
            Commit = buildInfo.LatestCommit.Substring(0, 6);
            Tag = buildInfo.LatestTag;
        }
    }
}
