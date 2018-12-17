/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 22:45:41 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.Util.I18N;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management.Filters;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMExtensions : ViewModelBase
    {

        #region MVVM
        public IEnumerable<ExtensionWrapperDock> Docks
        {
            get => _docks; set
            {
                _docks = value;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<ExtensionWrapperDock> _docks;

        public ExtensionWrapperDock SelectedDock
        {
            get => _selectedDock; set
            {
                _selectedDock = value;
                RaisePropertyChanged();
            }
        }
        private ExtensionWrapperDock _selectedDock;

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
            get => _extensions; set
            {
                _extensions = value;
                Docks = value.ToDocks();
                ExtensionsVisibily = _extensions.Count() == 0 ? Visibility.Collapsed : Visibility.Visible;
                NotFoundVisibily = _extensions.Count() == 0 ? Visibility.Visible : Visibility.Collapsed;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<IExtensionWrapper> _extensions;

        public ICommand RunSelectedItem { get; private set; }

        public ICommand GotoDownloadExtension { get; private set; }
        #endregion

        #region Device
        public void OnSelectNoDevice(object sender, EventArgs e)
        {
            ExtPanelIsEnabled = false;
        }

        public void OnSelectDevice(object sender, EventArgs e)
        {
            ExtPanelIsEnabled = (targetState & DeviceSelectionObserver.Instance.CurrentDevice.State) != 0;
        }

        #endregion
        private DeviceState targetState;

        public VMExtensions()
        {
            GotoDownloadExtension = new OpenParameterUrlCommand();
        }

        internal void Load(DeviceState state)
        {
            targetState = state;
            ComObserver();
            RunSelectedItem = new FlexiableCommand(() =>
            {
                SelectedDock?.Wrapper?.GetThread().Start();
            });
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
            ExtensionViewRefresher.Instance.Refreshing += (s, e) =>
            {
                LoadExtensions();
            };
            if (targetState == AutumnBoxExtension.NoMatter)
            {
                ExtPanelIsEnabled = true;
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
            IEnumerable<IExtensionWrapper> filted =
                OpenFramework.Management.Manager.InternalManager
                .GetLoadedWrappers(
                new DeviceStateFilter(targetState),
                CurrentRegionFilter.Singleton,
                DevelopingFilter.Singleton
                );
            App.Current.Dispatcher.Invoke(() =>
            {
                Extensions = filted;
            });
        }
    }
}
