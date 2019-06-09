using AutumnBox.Logging;
using AutumnBox.OpenFramework.Open.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 分发API
    /// </summary>
    public static class ApiFactory
    {
        /// <summary>
        /// 供静态类使用的API获取
        /// </summary>
        /// <typeparam name="TApi"></typeparam>
        /// <param name="staticClassType"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static TApi SGet<TApi>(Type staticClassType, object arg = null)
        {
            return OpenApiFactory.SGet<TApi>(staticClassType, arg);
        }

        /// <summary>
        /// 供静态类使用的API获取
        /// </summary>
        /// <param name="apiType"></param>
        /// <param name="staticClassType"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static object SGet(Type apiType, Type staticClassType = null, object arg = null)
        {
            return OpenApiFactory.SGet(apiType, staticClassType, arg);
        }

        /// <summary>
        /// 泛型的API获取方法
        /// </summary>
        /// <typeparam name="TApi"></typeparam>
        /// <param name="requester"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static TApi Get<TApi>(object requester = null, object arg = null)
        {
            return OpenApiFactory.Get<TApi>(requester, arg);
        }

        /// <summary>
        /// 平凡的,常见的API获取方法
        /// </summary>
        /// <param name="apiType"></param>
        /// <param name="requester"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static object Get(Type apiType, object requester, object arg = null)
        {
            return OpenApiFactory.Get(apiType, requester, arg);
        }
    }
}
