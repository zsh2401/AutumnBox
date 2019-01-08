using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Open;
using System;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    internal static class ApiAllocator
    {
        public static object GetProperty(Context ctx, Type propertyType)
        {
            if (propertyType == typeof(ILogger))
            {
                return ctx.Logger;
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
            return null;
        }
    }
}
