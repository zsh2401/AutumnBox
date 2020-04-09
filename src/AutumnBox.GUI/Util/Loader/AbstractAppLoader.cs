using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.Util.OS;
using AutumnBox.Logging;
using AutumnBox.Logging.Management;
using AutumnBox.OpenFramework;
using HandyControl.Data;
using HandyControl.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.Util.Loader
{
    internal abstract class AbstractAppLoader
    {
        public event EventHandler<StepFinishedEventArgs> StepFinished;
        public event EventHandler Succeced;
        public event EventHandler Failed;

        private readonly IEnumerable<MethodInfo> stepMethods;

        protected ILogger Logger { get; }

        internal AbstractAppLoader()
        {
            Logger = LoggerFactory.Auto(this.GetType().Name);
            stepMethods = FindStepMethods();
        }

        private const BindingFlags FINDING_FLAGS = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
        protected virtual IEnumerable<MethodInfo> FindStepMethods()
        {
            return from method in GetType().GetMethods(FINDING_FLAGS)
                   where method.GetCustomAttribute<StepAttribute>() != null
                   orderby method.GetCustomAttribute<StepAttribute>().Step ascending
                   select method;
        }

        public async Task LoadAsync()
        {
            await Task.Run(Load);
        }

        public void Load()
        {
            for (int i = 0; i < stepMethods.Count(); i++)
            {
                Logger.Info($"EXECUTING:: {stepMethods.ElementAt(i).Name}");
                stepMethods.ElementAt(i).Invoke(this, new object[0]);
                Logger.Info($"EXECUTED:: {stepMethods.ElementAt(i).Name}");
                StepFinished?.Invoke(this, new StepFinishedEventArgs((uint)i, (uint)stepMethods.Count()));
            }
            Succeced?.Invoke(this, new EventArgs());
        }

        protected virtual void OnError(string msg, Exception e)
        {
            Logger.Warn("Can not load app!");
            Logger.Warn(msg, e);
            Failed?.Invoke(this, new EventArgs());
            App.Current.Dispatcher.Invoke(() =>
            {
                //MessageBox.Show(e.ToString());
                App.Current.Shutdown(1);
            });
        }
    }
}
