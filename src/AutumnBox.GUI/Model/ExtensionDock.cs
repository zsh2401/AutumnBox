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
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.OpenFramework.Management.ExtInfo;
using AutumnBox.Logging;

namespace AutumnBox.GUI.Model
{
    internal class ExtensionDock : ModelBase
    {
        public string Name => ExtensionInfo.Name();

        public string ToolTip
        {
            get
            {
                return $"{ExtensionInfo.Name()}{Environment.NewLine}{ExtensionInfo.Author()}";
            }
        }

        public IExtensionInfo ExtensionInfo { get; }


        public ImageSource Icon
        {
            get
            {
                if (icon == null)
                {
                    icon = ExtensionInfo.Icon().ToExtensionIcon();
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
        private readonly INotificationManager notificationManager;

        public ExtensionDock(IExtensionInfo extInf)
        {
            this.ExtensionInfo = extInf;
            RootVisibily = extInf.NeedRoot() ? Visibility.Visible : Visibility.Collapsed;

            Execute = new FlexiableCommand(p =>
            {
                ExecuteImpl();
            });
            devicesManager.DeviceSelectionChanged += DeviceSelectionChanged;
        }

        private void DeviceSelectionChanged(object sender, EventArgs e)
        {
            SLogger.Info($"{ExtensionInfo.Name()}'s extension dock", "device selection changed: " + (devicesManager.SelectedDevice?.ToString() ?? "none"));
            SLogger.Info($"{ExtensionInfo.Name()}'s extension dock", $"runnable?{ExtensionInfo.IsRunnableCheck(devicesManager.SelectedDevice)}");
            Execute.CanExecuteProp = ExtensionInfo.IsRunnableCheck(devicesManager.SelectedDevice);
        }

        private void ExecuteImpl()
        {
            try
            {
                this.GetComponent<IExtensionTaskManager>().Start(ExtensionInfo);
            }
            catch (DeviceStateIsNotCorrectException)
            {
                notificationManager.Warn(GetDeviceStateNotCorrectWarningMessage());
            }
        }

        private string GetDeviceStateNotCorrectWarningMessage()
        {
            var reqState = ExtensionInfo.RequiredDeviceState();
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
        public static IEnumerable<ExtensionDock> ToDocks(this IEnumerable<IExtensionInfo> extInfos)
        {
            List<ExtensionDock> result = new List<ExtensionDock>();
            foreach (var inf in extInfos)
            {
                result.Add(new ExtensionDock(inf));
            }
            return result;
        }
    }
}
