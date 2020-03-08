using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension.Leaf.Attributes;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.LKit;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutumnBox.OpenFramework.Extension.Leaf.Internal
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
                return LakeProvider.Lake.Get<IBaseApi>().NewLeafUI();
            }
            else if (type.Name.StartsWith(nameof(ILogger)))
            {
                return LoggerFactory.Auto(leafInstance.GetType().Name);
            }
            else if (type == typeof(IDevice))
            {
                return LakeProvider.Lake.Get<IBaseApi>().SelectedDevice;
            }
            else if (type == typeof(Dictionary<string, object>))
            {
                return ExtData;
            }
            else if (type == typeof(ICommandExecutor))
            {
                return new HestExecutor();
            }
            else
            {
                return LakeProvider.Lake.Get(type);
            }
        }
    }
}
