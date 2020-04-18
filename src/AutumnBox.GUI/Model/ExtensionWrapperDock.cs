/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 11:52:25 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using AutumnBox.GUI.Services;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Management.ExtTask;

namespace AutumnBox.GUI.Model
{
    internal class ExtensionWrapperDock : ModelBase
    {
        public string ToolTip
        {
            get
            {
                return Wrapper.Info.Name + Environment.NewLine +
                     Wrapper.Info.FormatedDesc;
            }
        }
        public IExtensionWrapper Wrapper { get; private set; }
        public string Name => Wrapper.Info.Name;
        public IExtensionInfoDictionary Info => Wrapper.Info;
        public ImageSource Icon
        {
            get
            {
                if (icon == null)
                {
                    icon = Wrapper.Info.Icon.ToExtensionIcon();
                }
                return icon;
            }
        }
        private ImageSource icon;
        public Visibility RootVisibily
        {
            get => _rootVisibily; set
            {
                _rootVisibily = value;
                RaisePropertyChanged();
            }
        }
        private Visibility _rootVisibily;

        public FlexiableCommand Execute
        {
            get => _execute; set
            {
                _execute = value;
                RaisePropertyChanged();
            }
        }
        private FlexiableCommand _execute;

        [AutoInject]
        private readonly IAdbDevicesManager devicesManager;

        [AutoInject]
        private readonly Services.INotificationManager notificationManager;

        public ExtensionWrapperDock(IExtensionWrapper wrapper)
        {
            this.Wrapper = wrapper;
            bool requiredRoot = wrapper.Info[ExtensionMetadataKeys.ROOT] as bool? ?? false;
            RootVisibily = requiredRoot ? Visibility.Visible : Visibility.Collapsed;
            Execute = new FlexiableCommand(p =>
            {
                ExecuteImpl();
            });
            devicesManager.DeviceSelectionChanged += SelectedDevice;
        }

        private void SelectedDevice(object sender, EventArgs e)
        {
            if (devicesManager.SelectedDevice == null)
            {
                bool isNM = Wrapper.Info.RequiredDeviceStates == AutumnBoxExtension.NoMatter;
                Execute.CanExecuteProp = isNM;
            }
            else
            {
                Execute.CanExecuteProp = true;
            }
        }

        private void ExecuteImpl()
        {
            if (StateCheck())
            {
                this.GetComponent<IExtensionTaskManager>().Start(Wrapper.ExtensionType);
            }
        }
        private bool StateCheck()
        {
            var reqState = Wrapper.Info.RequiredDeviceStates;
            var crtState = devicesManager.SelectedDevice?.State ?? 0;
            if (reqState == LeafConstants.NoMatter) return true;
            else if (reqState.HasFlag(crtState)) return true;
            else
            {
                notificationManager.Warn(GetTip());
                return false;
            }
        }
        private string GetTip()
        {
            var reqState = Wrapper.Info.RequiredDeviceStates;
            List<string> statesString = new List<string>();
            if (reqState.HasFlag(DeviceState.Poweron))
                statesString.Add(App.Current.Resources[$"Dash.State.Poweron"].ToString());
            if (reqState.HasFlag(DeviceState.Recovery))
                statesString.Add(App.Current.Resources[$"Dash.State.Recovery"].ToString());
            if (reqState.HasFlag(DeviceState.Fastboot))
                statesString.Add(App.Current.Resources[$"Dash.State.Fastboot"].ToString());
            if (reqState.HasFlag(DeviceState.Sideload))
                statesString.Add(App.Current.Resources[$"Dash.State.Sideload"].ToString());
            if (reqState.HasFlag(DeviceState.Unauthorized))
                statesString.Add(App.Current.Resources[$"Dash.State.Unauthorized"].ToString());
            if (reqState.HasFlag(DeviceState.Offline))
                statesString.Add(App.Current.Resources[$"Dash.State.Offline"].ToString());
            if (reqState.HasFlag(DeviceState.Unknown))
                statesString.Add(App.Current.Resources[$"Dash.State.Unknown"].ToString());
            string fmt = App.Current.Resources[$"StateNotRightTipFmt"].ToString();
            return string.Format(fmt, string.Join(",", statesString.ToArray()));
        }
    }
    internal static class ExtensionWrapperDockExtensions
    {
        public static IEnumerable<ExtensionWrapperDock> ToDocks(this IEnumerable<IExtensionWrapper> wrappers)
        {
            List<ExtensionWrapperDock> result = new List<ExtensionWrapperDock>();
            foreach (var wrapper in wrappers)
            {
                result.Add(new ExtensionWrapperDock(wrapper));
            }
            return result;
        }
    }
}
