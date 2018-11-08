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
        /// <param name="name"></param>
        /// <returns></returns>
        AtmbService GetServiceByName(Context ctx,string name);
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
