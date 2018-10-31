/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/23 14:28:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Service
{
    /// <summary>
    /// 标准秋之盒服务基类
    /// </summary>
    [ServiceName(null)]
    public abstract class AtmbService : Context, IEquatable<AtmbService>
    {
        /// <summary>
        /// 服务名
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        public AtmbService()
        {
            Name = GetServiceName(this.GetType());
        }
        /// <summary>
        /// 检测是否是秋之盒标准服务type
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static bool IsAtmbServiceType(Type serviceType)
        {
            return serviceType.IsSubclassOf(typeof(AtmbService));
        }
        /// <summary>
        /// 获取服务名
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static string GetServiceName(Type serviceType)
        {
            if (!IsAtmbServiceType(serviceType))
            {
                throw new Exception("This is not a atmb service class");
            }
            ServiceName sn = (ServiceName)
                Attribute.GetCustomAttribute
                (serviceType, typeof(ServiceName));
            serviceType.GetCustomAttributes(true);
            if (sn.Name == null)
            {
                throw new NullReferenceException("Service name can't be null!");
            }
            return sn.Name;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public ServiceState State { get; set; } = ServiceState.Ready;

        internal void Start()
        {
            if (State == ServiceState.Running)
            {
                throw new InvalidOperationException("This service is already started");
            }
            State = ServiceState.Running;
            OnStartCommand(null);
        }

        internal void Stop()
        {
            if (State == ServiceState.Stopped)
            {
                throw new InvalidOperationException("This service is already stopped");
            }
            if (State == ServiceState.Ready)
            {
                throw new InvalidOperationException("This service is never been started");
            }
            State = ServiceState.Stopped;
            OnStopCommand(null);

        }

        internal void Destory()
        {
            OnDestory(null);
        }

        /// <summary>
        /// 当服务实例被创建时调用
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnCreate(object[] args) { }
        /// <summary>
        /// 当服务被请求启动时调用,此方法将在主线程调用,如需覆写请勿阻塞
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnStartCommand(object[] args)
        {
        }
        /// <summary>
        /// 当服务被要求停止时调用
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnStopCommand(object[] args) { }
        /// <summary>
        /// 当服务被摧毁时调用
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnDestory(object[] args)
        {
        }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public override bool Equals(object obj)
        {
            return Equals(obj as AtmbService);
        }

        public bool Equals(AtmbService other)
        {
            return other != null &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public static bool operator ==(AtmbService service1, AtmbService service2)
        {
            return EqualityComparer<AtmbService>.Default.Equals(service1, service2);
        }

        public static bool operator !=(AtmbService service1, AtmbService service2)
        {
            return !(service1 == service2);
        }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
