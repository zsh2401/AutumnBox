using AutumnBox.OpenFramework.Management;
using System;
using System.Diagnostics;

namespace AutumnBox.OpenFramework.LeafExtension.Kit.Impl
{
    class LeafLogger : ILeafLogger
    {
        private readonly string categoryName;

        private protected LeafLogger(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentException("message", nameof(categoryName));
            }


            this.categoryName = categoryName;
        }

        public static object From(Type leafType, Type targetType)
        {
            Type[] genericArgs = targetType.GetGenericArguments();
            bool isGLogger = genericArgs.Length == 1;
            if (isGLogger)
            {
                Trace.WriteLine("is logger");
                Type loggerT = typeof(LeafLogger<>);
                loggerT = loggerT.MakeGenericType(genericArgs[0]);
                return Activator.CreateInstance(loggerT);
            }
            else
            {
                return new LeafLogger(leafType.Name);
            }
        }

        public void Debug(object message)
        {
            if (!OpenFx.BaseApi.IsDeveloperMode) return;
            Log(nameof(Debug), message?.ToString());
        }

        public void Debug(object message, Exception e)
        {
            if (!OpenFx.BaseApi.IsDeveloperMode) return;
            Log(nameof(Debug), $"{message}{Environment.NewLine}{e}");
        }

        public void Exception(Exception e)
        {
            Log(nameof(Exception), e?.ToString());
        }

        public void Fatal(object message)
        {
            Log(nameof(Fatal), message?.ToString());
        }

        public void Fatal(object message, Exception ex)
        {
            Log(nameof(Fatal), $"{message}{Environment.NewLine}{ex}");
        }

        public void Info(object message)
        {
            Log(nameof(Info), message?.ToString());
        }

        public void Log(string TAG, string message)
        {
            OpenFx.BaseApi.Log(categoryName, TAG, message);
        }

        public void Warn(object message)
        {
            Log(nameof(Warn), message?.ToString());
        }

        public void Warn(object message, Exception e)
        {
            Log(nameof(Warn), $"{message}{Environment.NewLine}{e}");
        }
    }
    class LeafLogger<TCategory> : LeafLogger, ILeafLogger<TCategory>
    {
        public LeafLogger() : base(typeof(TCategory).Name)
        {
        }
    }
}
