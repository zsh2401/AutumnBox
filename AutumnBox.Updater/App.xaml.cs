using AutumnBox.Updater.Core;
using AutumnBox.Updater.Core.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
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
        public App()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var executingAss = Assembly.GetExecutingAssembly();
            using (var resStream = executingAss.GetManifestResourceStream(executingAss.GetName().Name + ".InnerLib.Newtonsoft.Json.dll"))
            {
                byte[] buffer = new byte[resStream.Length];
                resStream.Read(buffer, 0, buffer.Length);
                return Assembly.Load(buffer);
            }
        }
    }
}
