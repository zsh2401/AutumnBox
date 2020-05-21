/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 2:22:20 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Leafx.Container.Support;
using AutumnBox.OpenFramework.Open;
using System;
using System.Diagnostics;
using System.Reflection;

namespace AutumnBox.OpenFramework.Implementation
{
    /// <summary>
    /// CompApi的实现
    /// </summary>
    [Component(Type = typeof(ICompApi))]
    class CompImpl : ICompApi
    {
        ///<inheritdoc/>
        public Version SdkVersion { get; set; }

        ///<inheritdoc/>
        public int ApiLevel
        {
            get
            {
                return SdkVersion.Major;
            }
        }

        ///<inheritdoc/>
        public void IsolatedInvoke(Action act)
        {
            act?.Invoke();
        }

        ///<inheritdoc/>
        public void IsolatedInvoke(int minSdk, Action act)
        {
            IsolatedInvoke(BuildInfo.API_LEVEL >= minSdk, act);
        }

        ///<inheritdoc/>
        public void IsolatedInvoke(bool canRun, Action act)
        {
            if (canRun)
            {
                IsolatedInvoke(act);
            }
        }

        ///<inheritdoc/>
        public CompImpl()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(asm.Location);
            SdkVersion = new Version(info.FileMajorPart, info.FileMinorPart, info.FileBuildPart);
        }
    }
}
