/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:19:53 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework
{
    public abstract class AutumnBoxExtendModule
    {
        public abstract string Name { get; }
        public virtual string Desc { get; } = "...";
        public virtual Version Version { get; } = new Version(1,0,0,0);
        public virtual int TargetSdk { get; } = 5;
        public virtual int MinSdk { get; } = 4;
        public virtual DeviceState RequiredDeviceState { get; } = DeviceState.Poweron;

        protected void Log(string message) {
            OpenApi.Log.Log(Name, message);
        }
        public virtual void Init(InitArgs args) { }
        public abstract void Run(RunArgs args);
        public virtual void Destory(DestoryArgs args) { }
    }
}
