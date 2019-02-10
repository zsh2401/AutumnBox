/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/19 19:22:23 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.Logging;
using System;

namespace AutumnBox.GUI.Util.Bus
{
    class OpenFxObserver
    {
        public static readonly OpenFxObserver Instance;
        static OpenFxObserver()
        {
            Instance = new OpenFxObserver();
        }
        private OpenFxObserver()
        {
        }
        public void OnLoaded()
        {
           
            if (IsLoaded)
            {
                return;
            }

            IsLoaded = true;
            App.Current.Dispatcher.Invoke(() =>
            {
                SLogger<OpenFxObserver>.Debug("Raising Loaded Event");
                LoadedSource?.Invoke(this, new EventArgs());
            });
        }
        public event EventHandler Loaded
        {
            add
            {
                if (IsLoaded)
                {
                    value?.Invoke(this, new EventArgs());
                    //LoadedSource += value;
                }
                else
                {
                    LoadedSource += value;
                }
            }
            remove
            {
                LoadedSource -= value;
            }
        }
        private event EventHandler LoadedSource;
        public bool IsLoaded { get; private set; } = false;
    }
}
