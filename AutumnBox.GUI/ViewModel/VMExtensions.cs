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
using AutumnBox.OpenFramework.Warpper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        #region WW
        public class WarpperWarpper
        {
            public IExtensionWarpper Warpper { get; private set; }
            public string Name => Warpper.Info.Name;
            public ImageSource Icon
            {
                get
                {
                    if (icon == null) LoadIcon();
                    return icon;
                }
            }
            private ImageSource icon;
            private WarpperWarpper(IExtensionWarpper warpper)
            {
                this.Warpper = warpper;
            }
            private void LoadIcon()
            {
                if (Warpper.Info.Icon == null)
                {
                    icon = App.Current.Resources["DefaultExtensionIcon"] as ImageSource;
                }
                else
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.StreamSource = new MemoryStream(Warpper.Info.Icon);
                    bmp.EndInit();
                    bmp.Freeze();
                    icon = bmp;
                }
            }
            public static IEnumerable<WarpperWarpper> From(IEnumerable<IExtensionWarpper> warppers)
            {
                List<WarpperWarpper> result = new List<WarpperWarpper>();
                foreach (var warpper in warppers)
                {
                    result.Add(new WarpperWarpper(warpper));
                }
                return result;
            }
        }
        #endregion

        #region MVVM
        public IEnumerable<WarpperWarpper> Warppers
        {
            get
            {
                return ww;
            }
            set
            {
                ww = value;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<WarpperWarpper> ww;

        public ICommand RunExtension => _runExtension;
        private FlexiableCommand _runExtension;

        public WarpperWarpper Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (value == null)
                {
                    DetailsVisibily = Visibility.Collapsed;
                }
                else
                {
                    DetailsVisibily = Visibility.Visible;
                }
                _selected = value;
                RaisePropertyChanged();
            }
        }
        private WarpperWarpper _selected;

        public Visibility DetailsVisibily
        {
            get
            {
                return _detailsVisi;
            }
            set
            {
                _detailsVisi = value;
                RaisePropertyChanged();
            }
        }
        private Visibility _detailsVisi = Visibility.Collapsed;

        public Visibility BaseVisibily
        {
            get
            {
                return _baseVisi;
            }
            set
            {
                _baseVisi = value;
                RaisePropertyChanged();
            }
        }
        private Visibility _baseVisi = Visibility.Visible;

        public Visibility ExtensionsVisibily
        {
            get
            {
                return _extsVisi;
            }
            set
            {
                _extsVisi = value;
                RaisePropertyChanged();
            }
        }
        private Visibility _extsVisi = Visibility.Collapsed;

        public string BtnRunExtensionContent
        {
            get
            {
                return _btnContent;
            }
            set
            {
                _btnContent = value;
                RaisePropertyChanged();
            }
        }
        private string _btnContent;

        public ICommand GotoDownloadExtension { get; private set; }
        #endregion

        #region Device
        public void OnSelectNoDevice(object sender, EventArgs e)
        {
            Selected = null;
            BtnStatus = false;
        }

        public void OnSelectDevice(object sender, EventArgs e)
        {
            Selected = null;
            BtnStatus = (targetState & DeviceSelectionObserver.Instance.CurrentDevice.State) != 0;
        }

        #endregion
        private bool BtnStatus
        {
            set
            {
                BtnRunExtensionContent = value ? App.Current.Resources["PanelExtensionsButtonEnabled"].ToString() : App.Current.Resources["PanelExtensionsButtonDisabled"].ToString();
                _runExtension.CanExecuteProp = value;
            }
        }
        private DeviceState targetState;

        public VMExtensions(DeviceState targetState)
        {
            this.targetState = targetState;
            _runExtension = new FlexiableCommand((args) =>
            {
                Selected.Warpper.RunAsync(DeviceSelectionObserver.Instance.CurrentDevice);
            });
            GotoDownloadExtension = new OpenParameterUrlCommand();
            Selected = null;
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
                BtnStatus = true;
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
            IEnumerable<IExtensionWarpper> filted;
            if (targetState == AutumnBoxExtension.NoMatter)
            {
                filted = from warpper in OpenFramework.Management.Manager.InternalManager.Warppers
                         where warpper.Info.RequiredDeviceStates == 0
                         select warpper;
            }
            else
            {
                filted = from warpper in OpenFramework.Management.Manager.InternalManager.Warppers
                         where warpper.Info.RequiredDeviceStates.HasFlag(targetState)
                         select warpper;
            }

            Selected = null;
            App.Current.Dispatcher.Invoke(() =>
            {
                Warppers = WarpperWarpper.From(filted);
                if (Warppers.Count() == 0)
                {
                    BaseVisibily = Visibility.Visible;
                    ExtensionsVisibily = Visibility.Collapsed;
                }
                else
                {
                    BaseVisibily = Visibility.Collapsed;
                    ExtensionsVisibily = Visibility.Visible;
                }
            });
        }
    }
}
