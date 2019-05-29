using AutumnBox.OpenFramework.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open.Impl
{
    class ResManagerImpl : IResourcesManager
    {
        private readonly IBaseApi baseApi;
        public ResManagerImpl()
        {
            baseApi = OpenFx.BaseApi;
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
