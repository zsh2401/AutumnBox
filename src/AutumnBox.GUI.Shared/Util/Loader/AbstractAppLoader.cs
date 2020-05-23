using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
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
                    Logger.Info($"Loading: {stepMethods.ElementAt(i).Name}()");
                    var methodProxy = new MethodProxy(this, stepMethods.ElementAt(i), App.Current.Lake);
                    methodProxy.Invoke();
                    StepFinished?.Invoke(this, new StepFinishedEventArgs((uint)i, (uint)stepMethods.Count()));
                }
                catch (Exception e)
                {
                    OnError("Uncaught error", new AppLoadingException(stepMethods.ElementAt(i).Name, e));
                    return;
                }
            }
            Logger.Info("All done!");
            Succeced?.Invoke(this, new EventArgs());
        }

        protected virtual void OnError(string msg, AppLoadingException e)
        {
            Logger.Warn($"can't load application: {msg}");
            Logger.Warn($"message: {e.InnerException}");
            Logger.Warn($"source: {e.InnerException.Source}");
            Logger.Warn($"stack_trace: {e.InnerException.StackTrace}");
            Thread.Sleep(200);//至少等待日志被写入到文件中
            Failed?.Invoke(this, new AppLoaderFailedEventArgs(e));
        }
    }
}
