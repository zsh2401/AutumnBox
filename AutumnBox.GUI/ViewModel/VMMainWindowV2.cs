using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.Util.I18N;
using AutumnBox.GUI.Util.UI;
using AutumnBox.GUI.View.Controls;
using AutumnBox.GUI.View.Slices;
using AutumnBox.OpenFramework.Fast;
using AutumnBox.OpenFramework.Management;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SliceController = AutumnBox.GUI.Model.SliceController;

namespace AutumnBox.GUI.ViewModel
{
    class VMMainWindowV2 : ViewModelBase
    {

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
            base.RaisePropertyChangedOnDispatcher = true;
            InitTitle();
            LanguageManager.Instance.LanguageChanged += (s, e) =>
            {
                InitTitle();
            };
            AppLoader.Instance.Loaded += (s, e) =>
            {
                MainWindowBus.SwitchToMainGrid();
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
            //if (Self.HaveAdminPermission)
            //{
            //    Title += " " + App.Current.Resources["TitleSuffixAdmin"];
            //}
        }
    }
}
