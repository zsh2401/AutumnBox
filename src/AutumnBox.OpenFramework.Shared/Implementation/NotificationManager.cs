/*

* ==============================================================================
*
* Filename: NotificationManager
* Description: 
*
* Version: 1.0
* Created: 2020/3/17 0:31:29
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using System;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Implementation
{
    [Component(Type = typeof(INotificationManager))]
    class NotificationManager : INotificationManager
    {
        [AutoInject]
        private readonly IBaseApi baseApi;

        public Task<bool> Ask(string msg)
        {
            return baseApi.SendNotificationAsk(msg);
        }

        public void Info(string msg)
        {
            baseApi.SendNotificationInfo(msg);
        }


        public void Success(string msg)
        {
            baseApi.SendNotificationSuccess(msg);
        }

        public void Warn(string msg)
        {
            baseApi.SendNotificationWarn(msg);
        }
    }
}
