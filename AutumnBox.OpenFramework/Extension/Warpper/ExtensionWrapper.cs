/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 1:35:40 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension.Attributes;
using System;

namespace AutumnBox.OpenFramework.Extension
{
    internal class ExtensionWrapper : Context, IExtensionWarpper
    {
        private class ExtensionInfo
        {
            public readonly string Name;
            public readonly string Desc;
            public string FullDesc
            {
                get
                {
                    return null;
                }
            }
            public readonly string Auth;
            public readonly Version Version;
            public readonly DeviceState RequiredStates;
            public ExtensionInfo(Type type)
            {
            }
        }
        private ExtensionInfo info;
        private AutumnBoxExtension instance;
        private readonly Type extType;
        public string Name => info.Name;
        public string Desc => info.Name;
        public string Auth => info.FullDesc;
        private static readonly string DescFMT =
            "{0}: {1}" + Environment.NewLine +
            "{2}:" + Environment.NewLine +
            "{3}:";
        public override string LoggingTag => Name;

        internal ExtensionWrapper(Type t)
        {
            extType = t;
            info = new ExtensionInfo(t);
        }
        public virtual ForerunCheckResult ForerunCheck()
        {
            throw new NotImplementedException();
        }
        public virtual void Run(DeviceBasicInfo device)
        {
            var args = new CreatingArgs()
            {
                Deivce = device,
                LoggingTag = Name
            };
            instance = (AutumnBoxExtension)Activator.CreateInstance(extType, args);
            instance.Main();
        }
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
            return stopped;
        }
        public virtual void Destory()
        {

        }
    }
}