/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/14 21:47:35 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using System;

namespace AutumnBox.OpenFramework.Script
{
    [ScriptName("这个脚本的开发者是头猪")]
    [ScriptAuth("佚名")]
    [ScriptVersion(1, 0, 0, 0)]
    [ScriptContactInfo(null)]
    [ScriptUpdateInfo(Updatable = false)]
    [ScriptApiLevel(Min =7,Target =7)]
    /// <summary>
    /// AutumnBox拓展脚本
    /// </summary>
    public abstract class ABEScript : IExtensionScript
    {
        public abstract void Run(StartArgs args);
    }
}
