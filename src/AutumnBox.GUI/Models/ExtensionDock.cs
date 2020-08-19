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
using AutumnBox.Leafx.Container;

namespace AutumnBox.GUI.Models
{
    internal class ExtensionDock : ModelBase
    {
        public string Name => ExtensionInfo.Name();

        public string ToolTip
        {
            get
            {
                return $"{ExtensionInfo.Name()}{Environment.NewLine}v{ExtensionInfo.Version()}  by {ExtensionInfo.Author()}";
            }
        }

        public IExtensionInfo ExtensionInfo { get; }


        public ImageSource Icon
        {
            get
            {
                if (iconCache == null)
                {
                    iconCache = ExtensionInfo.Icon().ToExtensionIcon();
                }
                return iconCache;
            }
        }
        private ImageSource? iconCache;

        public Color RemarksColor
        {
            get
            {
                return _remarksColor;
            }
            set
            {
                _remarksColor = value;
                RaisePropertyChanged();
            }
        }
        private Color _remarksColor;

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; RaisePropertyChanged(); }
        }
        private string _remarks;

        public FlexiableCommand Execute
        {
            get => _execute; set
            {
                _execute = value;
                RaisePropertyChanged();
            }
        }
        private FlexiableCommand _execute;

        readonly IAdbDevicesManager devicesManager = App.Current.Lake.Get<IAdbDevicesManager>();

        readonly INotificationManager notificationManager = App.Current.Lake.Get<INotificationManager>();

        private const string MARK_NEED_ROOT = "ROOT";
        private const string MARK_NO_ROOT = "";

        public ExtensionDock(IExtensionInfo extInf)
        {
            this.ExtensionInfo = extInf;
            _remarksColor = extInf.NeedRoot() ? Colors.Red : Colors.GreenYellow;
            _remarks = extInf.NeedRoot() ? MARK_NEED_ROOT : MARK_NO_ROOT;
            _execute = new FlexiableCommand(p =>
            {
                ExecuteImpl();
            })
            {
                CanExecuteProp = ExtensionInfo.IsRunnableCheck(devicesManager?.SelectedDevice)
            };
            devicesManager!.DeviceSelectionChanged += DeviceSelectionChanged;
        }

        private void DeviceSelectionChanged(object? sender, EventArgs e)
        {
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
                statesString.Add(App.Current.Resources[$"Dash.State.Poweron"]?.ToString() ?? String.Empty);
            if (reqState.HasFlag(DeviceState.Recovery))
                statesString.Add(App.Current.Resources[$"Dash.State.Recovery"]?.ToString() ?? String.Empty);
            if (reqState.HasFlag(DeviceState.Fastboot))
                statesString.Add(App.Current.Resources[$"Dash.State.Fastboot"]?.ToString() ?? String.Empty);
            if (reqState.HasFlag(DeviceState.Sideload))
                statesString.Add(App.Current.Resources[$"Dash.State.Sideload"]?.ToString() ?? String.Empty);
            if (reqState.HasFlag(DeviceState.Unauthorized))
                statesString.Add(App.Current.Resources[$"Dash.State.Unauthorized"]?.ToString() ?? String.Empty);
            if (reqState.HasFlag(DeviceState.Offline))
                statesString.Add(App.Current.Resources[$"Dash.State.Offline"]?.ToString() ?? String.Empty);
            if (reqState.HasFlag(DeviceState.Unknown))
                statesString.Add(App.Current.Resources[$"Dash.State.Unknown"]?.ToString() ?? String.Empty);
            string fmt = App.Current.Resources[$"StateNotRightTipFmt"]?.ToString() ?? String.Empty;
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
