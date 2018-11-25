/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/7 20:55:48 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Service.Default;
using System;

namespace AutumnBox.OpenFramework.Management
{
    internal static class CallingBus
    {
        [ContextPermission(CtxPer.High)]
        private class CallingBusContext : Context { }
        private static readonly CallingBusContext ctx = new CallingBusContext();
        public static IBaseApi BaseApi
        {
            get
            {
                if (apiContainer == null)
                {
                    var service = Manager.ServicesManager.GetServiceByName(ctx, SBaseApiContainer.NAME);
                    apiContainer = (SBaseApiContainer)service;
                }
                return apiContainer.GetApi(new CallingBusContext());
            }
        }
        static SBaseApiContainer apiContainer;
    }
}
