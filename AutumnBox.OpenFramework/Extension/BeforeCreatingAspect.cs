/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/18 20:00:56 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    public class BeforeCreatingAspectArgs
    {
        public Context Context { get; set; }
        public Type ExtensionType { get; set; }
        public IDevice TargetDevice { get; set; }
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class BeforeCreatingAspect : Attribute
    {
        public abstract void Do(BeforeCreatingAspectArgs args, ref bool canContinue);
    }
}
