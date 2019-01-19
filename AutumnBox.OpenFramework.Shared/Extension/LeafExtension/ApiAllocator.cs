using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    internal static class ApiAllocator
    {
        public static object GetParamterValue(Dictionary<string, object> data, Type extType,Context ctx, ParameterInfo pInfo)
        {
            var fromDataAttr = pInfo.GetCustomAttribute<LFromDataAttribute>();
            if (fromDataAttr == null)
            {
                return GetProperty(ctx, extType, pInfo.ParameterType);
            }
            else
            {
                string key = fromDataAttr.Key ?? pInfo.Name;
                if (data.TryGetValue(key, out object value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
        }
        public static object GetProperty(Context ctx, Type extType, Type propertyType)
        {
            if (propertyType == typeof(ILogger))
            {
                return ctx.Logger;
            }
            if (propertyType == typeof(ILeafUI))
            {
                return CallingBus.BaseApi.NewLeafUI();
            }
            else if (propertyType == typeof(IUx))
            {
                return ctx.Ux;
            }
            else if (propertyType == typeof(IAppManager))
            {
                return ctx.App;
            }
            else if (propertyType == typeof(ITemporaryFloder))
            {
                return ctx.Tmp;
            }
            else if (propertyType == typeof(IEmbeddedFileManager))
            {
                return ctx.EmbeddedManager;
            }
            else if (propertyType == typeof(IOSApi))
            {
                return ctx.GetService<IOSApi>(ServicesNames.OS);
            }
            else if (propertyType == typeof(IDeviceSelector))
            {
                return ctx.GetService<IDeviceSelector>(ServicesNames.DEVICE_SELECTOR);
            }
            else if (propertyType == typeof(IMd5Service))
            {
                return ctx.GetService<IMd5Service>(ServicesNames.MD5);
            }
            else if (propertyType == typeof(IResourcesManager))
            {
                return ctx.GetService<IResourcesManager>(ServicesNames.RESOURCES);
            }
            else if (propertyType == typeof(ICompApi))
            {
                return ctx.Comp;
            }
            else if (propertyType == typeof(ISoundService))
            {
                return ctx.GetService<ISoundService>(ServicesNames.SOUND);
            }
            else if (propertyType == typeof(Context))
            {
                return ctx;
            }
            else if (propertyType == typeof(IDevice))
            {
                return ctx.GetService<IDeviceSelector>(ServicesNames.DEVICE_SELECTOR).GetCurrent(ctx);
            }
            else if (propertyType == typeof(TextAttrManager))
            {
                var m =  new TextAttrManager(extType);
                m.Load();
                return m;
            }
            return null;
        }
    }
}
