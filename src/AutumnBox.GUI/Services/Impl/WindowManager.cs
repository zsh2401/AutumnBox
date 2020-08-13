using AutumnBox.Leafx.Container.Support;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(IWindowManager))]
    class WindowManager : LoggingObject, IWindowManager
    {
        public Window MainWindow => App.Current.MainWindow;

        /// <summary>
        /// 创建窗口
        /// </summary>
        /// <param name="windowName"></param>
        /// <param name="args"></param>
        /// <exception cref="Exception">窗口未找到</exception>
        /// <returns></returns>
        public Window CreateWindow(string windowName, params object[] args)
        {
            if (Type.GetType("AutumnBox.GUI.Views.Windows." + windowName + "Window") is Type type
                &&
                Activator.CreateInstance(type, args) is Window win)
            {
                App.Current.MainWindow.Closed += (s, e) =>
                {
                    try
                    {
                        win.Close();
                    }
                    catch { }
                };
                return win;
            }
            else
            {
                throw new Exception("Window not found");
            }
        }

        public void Show(string windowName)
        {
            CreateWindow(windowName).Show();
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent("Show Window", new Dictionary<string, string>()
                {
                        { "Window Name",windowName},
                });
        }

        public void ShowDialog(string windowName)
        {
            CreateWindow(windowName).ShowDialog();
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent("Show Dialog Window", new Dictionary<string, string>()
                {
                        { "Window Name",windowName},
                });
        }
    }
}
