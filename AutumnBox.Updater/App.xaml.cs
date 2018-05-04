using AutumnBox.Updater.Core;
using AutumnBox.Updater.Core.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.Updater
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static new App Current => (App)Application.Current;
        internal readonly IUpdater UpdaterCore;
        public App() {
            UpdaterCore = new UpdaterImpl();
        }
    }
}
