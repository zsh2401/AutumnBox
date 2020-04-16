using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.Util.Loader
{
    internal abstract class AbstractAppLoader
    {
        public event EventHandler<StepFinishedEventArgs> StepFinished;
        public event EventHandler Succeced;
        public event EventHandler<AppLoaderFailedEventArgs> Failed;

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
                try
                {
                    Logger.Info($"BEGIN {stepMethods.ElementAt(i).Name}");
                    var methodProxy = new MethodProxy(this, stepMethods.ElementAt(i), App.Current.Lake);
                    methodProxy.Invoke();
                    Logger.Info($"END {stepMethods.ElementAt(i).Name}");
                    StepFinished?.Invoke(this, new StepFinishedEventArgs((uint)i, (uint)stepMethods.Count()));
                }
                catch (Exception e)
                {
                    OnError("Uncaught error", e);
                    return;
                }
            }
            Logger.Info("All done!");
            Succeced?.Invoke(this, new EventArgs());
        }

        protected virtual void OnError(string msg, Exception e)
        {
            Logger.Warn($"Can't load application: {msg}", e);
            Failed?.Invoke(this, new AppLoaderFailedEventArgs(e));
            App.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown(1);
            });
        }
    }
}
