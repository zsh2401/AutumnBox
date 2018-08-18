/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/16 13:36:23 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.GUI.Util.I18N;
using System;
using System.Collections.Generic;
using static AutumnBox.GUI.View.Panel.PanelDevices;

namespace AutumnBox.GUI.Depending
{
    internal class ListenerManager
    {
        public interface INotifyFxLoaded
        {
            event EventHandler FxLoaded;
        }
        public static readonly ListenerManager Instance;
        static ListenerManager()
        {
            Instance = new ListenerManager();
        }
        private class SavedState
        {
            public DeviceBasicInfo CurrentDevice { get; set; } = DeviceBasicInfo.None;
        }
        private readonly List<ILanguageChangedListener> LanguageChangedListeners;
        private readonly List<ISelectDeviceChangedListener> SelectDeviceChangedListeners;
        private readonly List<IOnExtensionFxLoadedListener> FxLoadedListeners;
        private readonly SavedState saved;
        private bool openFxLoaded = false;
        private ListenerManager()
        {
            LanguageChangedListeners = new List<ILanguageChangedListener>();
            SelectDeviceChangedListeners = new List<ISelectDeviceChangedListener>();
            FxLoadedListeners = new List<IOnExtensionFxLoadedListener>();
            saved = new SavedState();
        }
        public void Register(object listener)
        {
            if (listener == null) return;
            if (listener is ILanguageChangedListener langL)
            {
                LanguageChangedListeners.Add(langL);
            }
            if (listener is ISelectDeviceChangedListener dL)
            {
                SelectDeviceChangedListeners.Add(dL);
                dL.CurrentDevice = saved.CurrentDevice;
            }
            if (listener is IOnExtensionFxLoadedListener fL)
            {
                FxLoadedListeners.Add(fL);
                if (openFxLoaded)
                {
                    fL.OnExtensionFxLoaded();
                }
            }
        }
        public void Unregister(object listener)
        {
            if (listener == null) return;
            if (listener is ILanguageChangedListener langL)
            {
                LanguageChangedListeners.Remove(langL);
            }
            if (listener is ISelectDeviceChangedListener dL)
            {
                SelectDeviceChangedListeners.Remove(dL);
            }
            if (listener is IOnExtensionFxLoadedListener fL)
            {
                FxLoadedListeners.Remove(fL);
            }
        }
        public void RegisterEventSource(INotifyDeviceChanged notify, INotifyFxLoaded notifyFx)
        {
            LanguageManager.Instance.LanguageChanged += (s, e) =>
             {
                 var args = new LangChangedEventArgs()
                 {
                     NewLanguageCode = LanguageManager.Instance.Current.LanCode
                 };
                 LanguageChangedListeners.ForEach((listener) =>
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
            notifyFx.FxLoaded += (s, e) =>
            {
                openFxLoaded = true;
                FxLoadedListeners.ForEach((listener) =>
                {
                    listener.OnExtensionFxLoaded();
                });
            };
        }
        private void CallDeviceChangedListeners()
        {
            SelectDeviceChangedListeners.ForEach((l => CallDeviceChangedListener(l)));
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
