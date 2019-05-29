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
        private readonly Context ctx;
        private readonly Type leafType;

        public Dictionary<string, object> ExtData { get; set; }

        public ApiAllocator(Context ctx, Type leafType)
        {
            this.ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
            this.leafType = leafType ?? throw new ArgumentNullException(nameof(leafType));
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
                return LoggerFactory.Auto(type, leafType);
            }
            else if (type == typeof(IDevice))
            {
                return OpenFx.BaseApi.SelectedDevice;
            }
            else if (type == typeof(Context))
            {
                return ctx;
            }
            else if (type == typeof(TextAttrManager))
            {
                var m = new TextAttrManager(leafType);
                return m;
            }
            return null;

        }
    }
}
