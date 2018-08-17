/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/16 13:36:23 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.I18N;
using System.Collections.Generic;
using static AutumnBox.GUI.View.Panel.PanelDevices;

namespace AutumnBox.GUI.Depending
{
    static class ListenerManager
    {
        public readonly static List<ILanguageChangedListener> LanguageChangedListener;
        public readonly static List<ISelectDeviceChangedListener> SelectDeviceChangedListener;
        static ListenerManager()
        {
            LanguageChangedListener = new List<ILanguageChangedListener>();
            SelectDeviceChangedListener = new List<ISelectDeviceChangedListener>();

        }
        public static void Register(object listener)
        {
            if (listener == null) return;
            if (listener is ILanguageChangedListener langL)
            {
                LanguageChangedListener.Add(langL);
            }
            if (listener is ISelectDeviceChangedListener dL)
            {
                SelectDeviceChangedListener.Add(dL);
            }
        }
        public static void Unregister(object listener)
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
        public static void RegisterEventSource(INotifyDeviceChanged notify)
        {
            //LanguageHelper.LanguageChanged += (s, e) =>
            //{
            //    var args = new LangChangedEventArgs()
            //    {
            //        NewLanguageCode = App.Current.Resources["LangCode"].ToString()
            //    };
            //    LanguageChangedListener.ForEach((listener) =>
            //    {
            //        listener.OnLanguageChanged(args);
            //    });
            //};
            notify.DeviceChanged += (s, e) =>
            {
                var args = new SelectDeviceEventArgs()
                {
                    DeviceInfo = e.CurrentDevice
                };
                SelectDeviceChangedListener.ForEach(l => l.OnSelectDevice(args));
            };
            notify.NoDevice += (s, e) =>
            {
                SelectDeviceChangedListener.ForEach(l => l.OnSelectNoDevice());
            };
        }
    }
}
