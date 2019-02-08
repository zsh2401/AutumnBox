using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Service;
using AutumnBox.OpenFramework.Service.Default;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open.ServiceImpl
{
    [ServiceName(ServicesNames.RESOURCES)]
    internal class SResourcesManager : AtmbService, IResourcesManager
    {
        private readonly IBaseApi baseApi;
        public SResourcesManager()
        {
            var container = GetService<SBaseApiContainer>(SBaseApiContainer.NAME);
            baseApi = container.GetApi(this);
        }

        public object this[string key]
        {
            get
            {
                return baseApi.GetResouce(key);
            }
            set
            {
                baseApi.SetResource(key, value);
            }
        }

        public void Add(string key, object value)
        {
            baseApi.AddResource(key, value);
        }

        public TReturn Get<TReturn>(string key) where TReturn : class
        {
            return baseApi.GetResouce(key) as TReturn;
        }
        public TReturn GetValue<TReturn>(string key) where TReturn : struct
        {
            return (TReturn)baseApi.GetResouce(key);
        }
    }
}
