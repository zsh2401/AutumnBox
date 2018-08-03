/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/4 0:02:52 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using System;
using System.Linq;

namespace AutumnBox.OpenFramework.Extension
{
    internal class ExtensionInfoGetter
    {
        private readonly Context ctx;
        private readonly Type type;
        private static readonly string DescFMT =
       "{0}: {1}" + Environment.NewLine +
       "{2}:" + Environment.NewLine +
       "{3}:";
        public string Name;
        public string Desc;
        public string FullDesc
        {
            get
            {

                return string.Format(DescFMT,
                    ctx.App.GetPublicResouce("lbAuth"), Auth,
                    ctx.App.GetPublicResouce("lbDescription"), Desc);
            }
        }
        public string Auth;
        public Version Version;
        public DeviceState RequiredStates;
        public ExtensionInfoGetter(Context ctx, Type type)
        {
            this.ctx = ctx;
            this.type = type;
        }
        public void Load()
        {
            var attrs = type.GetCustomAttributes(true);
            ctx.Logger.Info($"getted {attrs.Count()} attr from {type.Name}");
            foreach (var attr in attrs)
            {
                if (attr is ExtNameAttribute nameAttr)
                {
                    Name = nameAttr.Name;
                }
                else if (attr is ExtAuthAttribute authAttr)
                {
                    Auth = authAttr.Auth;
                }
                else if (attr is ExtDescAttribute descAttr)
                {
                    Desc = descAttr.Desc;
                }
                else if (attr is ExtVersion versionAttr)
                {
                    Version = versionAttr.Version;
                }
                else if (attr is ExtRequiredDeviceStatesAttribute reqStatesAttr)
                {
                    RequiredStates = reqStatesAttr.Value;
                }
            }
        }
    }
}
