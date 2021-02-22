using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;
using AutumnBox.GUI.Util.Loader;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.ObjectManagement;
using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace AutumnBox.GUI.ViewModels
{
    class VMStartup : ViewModelBase
    {
        protected override bool InjectProperties => false;

        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                RaisePropertyChanged();
            }
        }
        private string _status;

        [AutoInject]
        private IBuildInfo build
        {
            get;
            set;
        }

        public string VersionName
        {
            get => _versionName;
            set
            {
                _versionName = value;
                RaisePropertyChanged();
            }
        }
        private string _versionName;

        public double Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                _progress = value;
                RaisePropertyChanged();
            }
        }
        private double _progress;

        public AppLoadingException Exception
        {
            get
            {
                return _exception;
            }
            set
            {
                _exception = value;
                RaisePropertyChanged();
            }
        }
        private AppLoadingException _exception;


        public bool Loaded
        {
            get
            {
                return _loaded;
            }
            set
            {
                _loaded = value;
                RaisePropertyChanged();
            }
        }
        private bool _loaded;

        public VMStartup()
        {
            RaisePropertyChangedOnUIThread = true;
            App.Current.AppLoader.StepFinished += AppLoader_StepFinished;
            App.Current.AppLoader.Succeced += AppLoader_Succeced;
            App.Current.AppLoader.Failed += AppLoader_Failed;
        }

        private void AppLoader_Failed(object sender, AppLoaderFailedEventArgs e)
        {
            Exception = e.Exception;
        }

        private void AppLoader_Succeced(object sender, System.EventArgs e)
        {
            Loaded = true;
        }

        private void AppLoader_StepFinished(object sender, StepFinishedEventArgs e)
        {
            Progress = 100.0 * e.FinishedStep / e.TotalStepCount;

            if (VersionName == null)
            {
                var build = App.Current.Lake.Get<IBuildInfo>();
                VersionName = build.Version +
#if CANARY
                    "-canary"
#elif DEBUG
                    "-debug"
#else
                    ""
#endif
                    ;
            }
        }
    }
}
