using AutumnBox.Logging;
using AutumnBox.OpenFramework.Open.Impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutumnBox.OpenFramework.Open.Management
{
    /// <summary>
    /// 开放API工厂,所有Open命名空间下的API都可由此获取
    /// </summary>
    public static class OpenApiFactory
    {
        private const string IMPLS_NAMESPACE = "AutumnBox.OpenFramework.Open.Impl";
        static readonly Dictionary<Type, Type> ApiAndImplsInformation = new Dictionary<Type, Type>();
        static OpenApiFactory()
        {
            ScanAndLoad();
        }
        /// <summary>
        /// 扫描并加载所有API与实现对应关系
        /// </summary>
        static void ScanAndLoad()
        {
            var types = from type in Assembly.GetCallingAssembly().GetTypes()
                        where type.Namespace == IMPLS_NAMESPACE
                        select type;
            foreach (var type in types)
            {
                foreach (var _interface in type.GetInterfaces())
                {
                    ApiAndImplsInformation.Add(_interface, type);
                }
            }
        }

        /// <summary>
        /// 供静态类使用的API获取
        /// </summary>
        /// <typeparam name="TApi"></typeparam>
        /// <param name="staticClassType"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static TApi SGet<TApi>(Type staticClassType, object arg = null)
        {
            return (TApi)Get(new ApiRequest(typeof(TApi), null, arg, staticClassType));
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
            return Get(new ApiRequest(apiType, null, arg, staticClassType));
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
            return (TApi)Get(new ApiRequest(typeof(TApi), requester, arg));
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
            return Get(new ApiRequest(apiType, requester, arg, null));
        }

        /// <summary>
        /// 获取API
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object Get(ApiRequest request)
        {
            if (ApiAndImplsInformation.TryGetValue(request.TargetApiType, out Type implType))
            {
                try
                {
                    return Activator.CreateInstance(implType, request);
                }
                catch (Exception ex)
                {
                    SLogger.Warn(nameof(OpenApiFactory), "could not create instance", ex);
                    return Activator.CreateInstance(implType);
                }
            }
            else
            {
                return null;
            }
        }
    }
}
