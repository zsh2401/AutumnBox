using AutumnBox.OpenFramework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Service
{
    /// <summary>
    /// 服务管理器
    /// </summary>
   public interface IServicesManager
    {
        /// <summary>
        /// 根据名称获取服务
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        AtmbService GetServiceByName(Context ctx,string serviceName);
        /// <summary>
        /// 根据泛型获取服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="ctx"></param>
        /// <returns></returns>
        AtmbService GetService<TService>(Context ctx);
        /// <summary>
        /// 根据服务名获取服务，并转换为传入的泛型
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="ctx">上下文,用于身份验证等</param>
        /// <param name="serviceName">要寻找的服务名</param>
        /// <returns></returns>
        TService GetServiceByName<TService>(Context ctx, string serviceName) where TService : AtmbService;
        /// <summary>
        /// 启动一个服务
        /// </summary>
        /// <param name="typeOfService"></param>
        void StartService(Type typeOfService);
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        void StartService<TService>() where TService : AtmbService,new();
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="typeOfService"></param>
        void StopService(Type typeOfService);
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        void StopService<TService>() where TService : AtmbService;
    }
}
