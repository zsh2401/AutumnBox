using AutumnBox.Logging.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Logging
{
    /// <summary>
    /// 日志器工厂
    /// </summary>
    public static class LoggerFactory
    {
        /// <summary>
        /// 通过传入的泛型参数自动获取
        /// </summary>
        /// <returns></returns>
        public static ILogger Auto<TCategoryClass>()
        {
            return new Internal.LoggerImpl(typeof(TCategoryClass).Name);
        }
        /// <summary>
        /// Auto
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public static ILogger Auto(string categoryName)
        {
            return new Internal.LoggerImpl(categoryName);
        }
        /// <summary>
        /// 获取Logger
        /// </summary>
        /// <typeparam name="TCategoryClass"></typeparam>
        /// <returns></returns>
        public static ILogger<TCategoryClass> AutoT<TCategoryClass>()
        {
            return new LoggerImpl<TCategoryClass>();
        }
        /// <summary>
        /// 通过需要的日志器类型,自动构造
        /// </summary>
        /// <param name="loggerType"></param>
        /// <returns></returns>
        public static object Auto(Type loggerType, Type defaultCategory = null)
        {
            Type[] genericArgs = loggerType.GetGenericArguments();
            bool isGLogger = genericArgs.Length == 1;
            if (isGLogger)
            {
                Type loggerT = typeof(ILogger<>);
                loggerT = loggerT.MakeGenericType(genericArgs[0]);
                return Activator.CreateInstance(loggerT);
            }
            else
            {
                return new LoggerImpl(defaultCategory.Name ?? "UnknownCategory");
            }
        }
    }
}
