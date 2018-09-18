/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 22:45:41 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.Util.I18N;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management.Filters;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.ViewModel
{
    class VMExtensions : ViewModelBase
    {

        #region MVVM
        public bool ExtPanelIsEnabled
        {
            get => _extPanelIsEnabled; set
            {
                _extPanelIsEnabled = value;
                RaisePropertyChanged();
            }
        }
        private bool _extPanelIsEnabled = false;
        public Visibility ExtensionsVisibily
        {
            get
            {
                return _extVisi;
            }
            set
            {
                _extVisi = value;
                RaisePropertyChanged();
            }
        }
        private Visibility _extVisi = Visibility.Collapsed;

        public Visibility NotFoundVisibily
        {
            get
            {
                return _notFoundVisi;
            }
            set
            {
                _notFoundVisi = value;
                RaisePropertyChanged();
            }
        }
        private Visibility _notFoundVisi = Visibility.Visible;

        public IEnumerable<IExtensionWrapper> Extensions
        {
            get => extensions; set
            {
                extensions = value;
                ExtensionsVisibily = extensions.Count() == 0 ? Visibility.Collapsed : Visibility.Visible;
                NotFoundVisibily = extensions.Count() == 0 ? Visibility.Visible : Visibility.Collapsed;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<IExtensionWrapper> extensions;

        public ICommand GotoDownloadExtension { get; private set; }
        #endregion

        #region Device
        public void OnSelectNoDevice(object sender, EventArgs e)
        {
            ExtPanelIsEnabled = false;
            //BtnStatus = false;
        }

        public void OnSelectDevice(object sender, EventArgs e)
        {
            ExtPanelIsEnabled = (targetState & DeviceSelectionObserver.Instance.CurrentDevice.State) != 0;
        }

        #endregion
        //private bool BtnStatus
        //{
        //    set
        //    {
        //        BtnRunExtensionContent = value ? App.Current.Resources["PanelExtensionsButtonEnabled"].ToString() : App.Current.Resources["PanelExtensionsButtonDisabled"].ToString();
        //        RunExtension.CanExecuteProp = value;
        //    }
        //}
        private DeviceState targetState;

        internal void Load(DeviceState state)
        {
            targetState = state;
            //RunExtension = new FlexiableCommand((args) =>
            //{
            //    Selected.Wrapper.RunAsync(DeviceSelectionObserver.Instance.CurrentDevice);
            //});
            GotoDownloadExtension = new OpenParameterUrlCommand();
            //Selected = null;
            ComObserver();
        }
        private void ComObserver()
        {
            if (OpenFxObserver.Instance.IsLoaded)
            {
                LoadExtensions();
            }
            else
            {
                OpenFxObserver.Instance.Loaded += (_, __) =>
                {
                    LoadExtensions();
                };
            }
            LanguageManager.Instance.LanguageChanged += (s, e) =>
            {
                LoadExtensions();
            };
            if (targetState == AutumnBoxExtension.NoMatter)
            {
                ExtPanelIsEnabled = true;
                //BtnStatus = true;
                return;
            }
            else
            {
                DeviceSelectionObserver.Instance.SelectedDevice += OnSelectDevice;
                DeviceSelectionObserver.Instance.SelectedNoDevice += OnSelectNoDevice;
            }
        }
        public void LoadExtensions()
        {
            //Selected = null;
            IEnumerable<IExtensionWrapper> filted =
                OpenFramework.Management.Manager.InternalManager
                .GetLoadedWrappers(
                new DeviceStateFilter(targetState),
                CurrentRegionFilter.Singleton
                );
            App.Current.Dispatcher.Invoke(() =>
            {
                Extensions = filted;
                //Wrappers = WrapperWrapper.From(filted);
                //if (Wrappers.Count() == 0)
                //{
                //    NotFoundVisibily = Visibility.Visible;
                //    ExtensionsVisibily = Visibility.Collapsed;
                //}
                //else
                //{
                //    NotFoundVisibily = Visibility.Collapsed;
                //    ExtensionsVisibily = Visibility.Visible;
                //}
            });
        }
    }
}
