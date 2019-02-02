using AutumnBox.OpenFramework.Management;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
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
            CallingBus.BaseApi.Log(categoryName, nameof(Debug), message?.ToString());
        }

        public void Exception(Exception e)
        {
            CallingBus.BaseApi.Log(categoryName, nameof(Exception), e?.ToString());
        }

        public void Fatal(object message)
        {
            CallingBus.BaseApi.Log(categoryName, nameof(Fatal), message?.ToString());
        }

        public void Fatal(object message, Exception ex)
        {
            CallingBus.BaseApi.Log(categoryName, nameof(Fatal), $"{message}{Environment.NewLine}{ex}");
        }

        public void Info(object message)
        {
            CallingBus.BaseApi.Log(categoryName, nameof(Info), message?.ToString());
        }

        public void Warn(object message)
        {
            CallingBus.BaseApi.Log(categoryName, nameof(Warn), message?.ToString());
        }

        public void Warn(object message, Exception e)
        {
            CallingBus.BaseApi.Log(categoryName, nameof(Warn), $"{message}{Environment.NewLine}{e}");
        }
    }
    class LeafLogger<TCategory> : LeafLogger, ILeafLogger<TCategory>
    {
        public LeafLogger() : base(typeof(TCategory).Name)
        {
        }
    }
}
