/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 1:35:40 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Management;
using System;
using System.Linq;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 标准的拓展模块包装器
    /// </summary>
    public class ExtensionWrapper : Context, IExtensionWarpper
    {
        private class ExtensionInfo
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
            public ExtensionInfo(Context ctx, Type type)
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
        private ExtensionInfo info;
        private AutumnBoxExtension instance;
        private readonly Type extType;
        public string Name => info.Name;
        public string Desc => info.FullDesc;
        public string Auth => info.Auth;
        public override string LoggingTag => Name;
        internal ExtensionWrapper(Type t)
        {
            extType = t;
            info = new ExtensionInfo(this, t);
            info.Load();
        }
        /// <summary>
        /// 运行前检查
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public virtual ForerunCheckResult ForerunCheck(DeviceBasicInfo device)
        {
            ForerunCheckResult result;
            if (info.RequiredStates.HasFlag(device.State))
            {
                result = ForerunCheckResult.Ok;
            }
            else
            {
                result = ForerunCheckResult.DeviceStateNotRight;
            }
            return result;
        }
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="device"></param>
        public virtual void Run(DeviceBasicInfo device)
        {
            if (instance != null)
            {
                App.RunOnUIThread(() =>
                {
                    App.ShowMessageBox("警告","该拓展模块已在运行,你不能开多个该模块!");
                });
            }
            instance = (AutumnBoxExtension)Activator.CreateInstance(extType);
            InjetctProperty(device);
            MainFlow();
        }
        private void InjetctProperty(DeviceBasicInfo device) {
            instance.TargetDevice = device;
            instance.ExtName = Name;
        }
        private void MainFlow() {
            RunningManager.AddRuningWarpper(this);
            instance.Main();
            instance = null;
            RunningManager.RemoveRunningWarpper(this);
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public virtual bool Stop()
        {
            bool stopped = false;
            try
            {
                stopped = instance.OnStopCommand();
            }
            catch (Exception ex)
            {
                Logger.Warn("停止时发生异常", ex);
            }
            if (stopped == true)
            {
                instance = null;
                RunningManager.RemoveRunningWarpper(this);
            }
            return stopped;
        }
        /// <summary>
        /// 当摧毁时被调用
        /// </summary>
        public virtual void Destory()
        {

        }
    }
}