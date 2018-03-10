/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/27 0:10:33 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Internal
{

    /// <summary>
    /// 拓展的运行管理器
    /// </summary>
    public class ExtensionRuntime
    {
        public string Name { get { return InnerExtension.Name; } }
        public readonly AutumnBoxExtension InnerExtension;
        public bool IsRuning { get; private set; }
        private ExtensionRuntime(AutumnBoxExtension ext)
        {
            this.InnerExtension = ext;
        }
        internal static ExtensionRuntime Create(Type type, InitArgs args = null)
        {
            Assembly
                .GetCallingAssembly()
                .AccessCheck(BuildInfo.AUTUMNBOX_GUI_ASSEMBLY_NAME, BuildInfo.AUTUMNBOX_OPENFRAMEWORK_ASSEMBLY_NAME);
            var ext = (AutumnBoxExtension)Activator.CreateInstance(type);
            if (!ext.InitAndCheck(args ?? new InitArgs())) throw new Exception("Cannot init");
            return new ExtensionRuntime(ext);
        }
        public async void RunAsync(StartArgs args, Action finishedCallback)
        {
            await Task.Run(() =>
            {
                Run(args);
            });
            finishedCallback();
        }
        public void Run(StartArgs args)
        {
            IsRuning = true;
            try
            {
                InnerExtension.OnStartCommand(args);
                InnerExtension.OnFinished();
            }
            catch (Exception ex)
            {
                OpenApi.Gui.ShowMessageBox("Warning", $"{InnerExtension.Name} was failed... \n{ex}");
            }
            finally
            {
                IsRuning = false;
            }
        }
        public bool Stop(StopArgs args = null)
        {
            if (!IsRuning) return true;
            return InnerExtension.OnStopCommand(args ?? new StopArgs());
        }
        public void Destory(DestoryArgs args = null)
        {
            InnerExtension.OnDestory(args ?? new DestoryArgs());
        }
        public void WaitForFinish()
        {
            while (IsRuning) ;
        }
    }
}
