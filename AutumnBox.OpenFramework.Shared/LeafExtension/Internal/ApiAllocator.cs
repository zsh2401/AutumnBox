using AutumnBox.Basic.Device;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Internal.Impl;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.Management;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutumnBox.OpenFramework.LeafExtension.Internal
{
    internal class ApiAllocator
    {
        private readonly LeafExtensionBase leafInstance;

        public Dictionary<string, object> ExtData { get; set; }

        public ApiAllocator(LeafExtensionBase leafInstance)
        {
            this.leafInstance = leafInstance ?? throw new ArgumentNullException(nameof(leafInstance));
        }

        public object GetParamterValue(ParameterInfo pInfo)
        {
            if (ExtData == null)
            {
                throw new InvalidOperationException("Have not set ExtData!");
            }
            var fromDataAttr = pInfo.GetCustomAttribute<LFromDataAttribute>();
            if (fromDataAttr == null)
            {
                return GetByType(pInfo.ParameterType);
            }
            else
            {
                string key = fromDataAttr.Key ?? pInfo.Name;
                if (ExtData.TryGetValue(key, out object value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
        }

        public object GetByType(Type type)
        {
            if (type == typeof(ILeafUI))
            {
                return OpenFx.BaseApi.NewLeafUI();
            }
            else if (type.Name.StartsWith(nameof(ILogger)))
            {
                return LoggerFactory.Auto(leafInstance.GetType().Name);
            }
            else if (type == typeof(IDevice))
            {
                return OpenFx.BaseApi.SelectedDevice;
            }
            else if (type == typeof(Dictionary<string, object>))
            {
                return ExtData;
            }
            else if (type == typeof(IEmbeddedFileManager))
            {
                return new LeafEmb(leafInstance.GetType().Assembly);
            }
            else
            {
                return OpenApiFactory.Get(type, leafInstance);
            }
        }
    }
}
