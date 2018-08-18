/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/16 13:36:23 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.GUI.Util.I18N;
using System.Collections.Generic;
using static AutumnBox.GUI.View.Panel.PanelDevices;

namespace AutumnBox.GUI.Depending
{
    internal class ListenerManager
    {
        public static readonly ListenerManager Instance;
        static ListenerManager()
        {
            Instance = new ListenerManager();
        }
        private class SavedState
        {
            public DeviceBasicInfo CurrentDevice { get; set; } = DeviceBasicInfo.None;
        }
        private readonly List<ILanguageChangedListener> LanguageChangedListener;
        private readonly List<ISelectDeviceChangedListener> SelectDeviceChangedListener;
        private readonly SavedState saved;
        private ListenerManager()
        {
            LanguageChangedListener = new List<ILanguageChangedListener>();
            SelectDeviceChangedListener = new List<ISelectDeviceChangedListener>();
            saved = new SavedState();
        }
        public void Register(object listener)
        {
            if (listener == null) return;
            if (listener is ILanguageChangedListener langL)
            {
                LanguageChangedListener.Add(langL);
            }
            if (listener is ISelectDeviceChangedListener dL)
            {
                SelectDeviceChangedListener.Add(dL);
                dL.CurrentDevice = saved.CurrentDevice;
            }
        }
        public void Unregister(object listener)
        {
            if (listener == null) return;
            if (listener is ILanguageChangedListener langL)
            {
                LanguageChangedListener.Remove(langL);
            }
            if (listener is ISelectDeviceChangedListener dL)
            {
                SelectDeviceChangedListener.Remove(dL);
            }
        }
        public void RegisterEventSource(INotifyDeviceChanged notify)
        {
            LanguageManager.Instance.LanguageChanged += (s, e) =>
             {
                 var args = new LangChangedEventArgs()
                 {
                     NewLanguageCode = LanguageManager.Instance.Current.LanCode
                 };
                 LanguageChangedListener.ForEach((listener) =>
                 {
                     listener.OnLanguageChanged(args);
                 });
             };
            notify.DeviceChanged += (s, e) =>
            {
                saved.CurrentDevice = e.CurrentDevice;
                CallDeviceChangedListeners();
            };
            notify.NoDevice += (s, e) =>
            {
                saved.CurrentDevice = DeviceBasicInfo.None;
                CallDeviceChangedListeners();
            };
        }
        private void CallDeviceChangedListeners()
        {
            SelectDeviceChangedListener.ForEach((l => CallDeviceChangedListener(l)));
        }
        private void CallDeviceChangedListener(ISelectDeviceChangedListener listener)
        {
            listener.CurrentDevice = saved.CurrentDevice;
            if (saved.CurrentDevice.State == DeviceState.None)
            {
                listener.OnSelectNoDevice();
            }
            else
            {
                listener.OnSelectDevice();
            }
        }
    }
}
