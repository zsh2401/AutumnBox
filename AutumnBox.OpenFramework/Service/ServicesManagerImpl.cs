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
using AutumnBox.OpenFramework.Exceptions;

namespace AutumnBox.OpenFramework.Service
{
    internal class ServicesManagerImpl : IServicesManager
    {
        internal readonly List<AtmbService> _serviceCollection;

        public ServicesManagerImpl()
        {
            _serviceCollection = new List<AtmbService>();
        }

        public AtmbService GetServiceByName(Context ctx, string name)
        {
            var result = from service in _serviceCollection
                         where service.Name == name
                         select service;
            if (result.Count() != 0)
            {
                if (!result.First().Get(ctx))
                {
                    throw new AccessDeniedException();
                }
                return result.First();
            }
            else
            {
                throw new ServiceNotFoundException(name);
            }
        }

        public AtmbService GetInstance(Type t)
        {
            var filtResult = from serv in _serviceCollection
                             where serv.GetType() == t
                             select serv;
            if (filtResult.Count() > 0)
            {
                return filtResult.First();
            }
            else
            {
                object _obj = Activator.CreateInstance(t);
                AtmbService instance = (AtmbService)_obj;
                _serviceCollection.Add(instance);
                return instance;
            }
        }

        public void StartService(Type typeOfService)
        {
            GetInstance(typeOfService).Start();
        }

        public void StartService<TService>() where TService : AtmbService, new()
        {
            StartService(typeof(TService));
        }

        public void StopService(Type typeOfService)
        {
            GetInstance(typeOfService).Stop();
        }

        public void StopService<TService>() where TService : AtmbService
        {
            StopService(typeof(TService));
        }

        public AtmbService GetService<TService>(Context ctx)
        {
            var servs = from serv in _serviceCollection
                        where serv is TService
                        select serv;
            if (servs.Count() == 0)
            {
                throw new ServiceNotFoundException("T" + typeof(TService).FullName);
            }
            return servs.First();
        }

        public TService GetServiceByName<TService>(Context ctx, string serviceName) where TService : AtmbService
        {
            return GetServiceByName(ctx, serviceName) as TService;
        }
    }
}
