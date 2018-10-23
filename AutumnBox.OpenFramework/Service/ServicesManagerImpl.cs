/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/23 14:38:44 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutumnBox.OpenFramework.Content;

namespace AutumnBox.OpenFramework.Service
{
    internal class ServicesManagerImpl : IServicesManager
    {
        private readonly List<AtmbService> _serviceCollection;

        public ServicesManagerImpl()
        {
            _serviceCollection = new List<AtmbService>();
        }

        public AtmbService GetServiceById(Context ctx, int id)
        {
            var result = from service in _serviceCollection
                         where service.Id == id
                         select service;
            if (result.Count() != 0)
            {
                return result.First();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public AtmbService GetServiceByName(Context ctx, string name)
        {
            var result = from service in _serviceCollection
                         where service.Name == name
                         select service;
            if (result.Count() != 0)
            {
                return result.First();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public void RegisterService(AtmbService service)
        {
            _serviceCollection.Add(service);
        }

        internal void StopAll() {
            _serviceCollection.ForEach((service) =>
            {
                if (service.State == ServiceState.Running)
                {
                    try { service.Stop(); } catch { }
                }
            });
        }

        internal void FreeAll()
        {
            _serviceCollection.ForEach((service) =>
            {
                if (service.State == ServiceState.Running)
                {
                    try { service.Stop(); } catch { }
                }
                try { service.Destory(); } catch { }
            });
        }
    }
}
