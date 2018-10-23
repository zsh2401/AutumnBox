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
        /// 根据ID获取服务
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        AtmbService GetServiceById(Context ctx,int id);
        /// <summary>
        /// 根据名称获取服务
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        AtmbService GetServiceByName(Context ctx,string name);
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="service"></param>
        void RegisterService(AtmbService service);
    }
}
