/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/27 0:10:33 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Internal.AccessCheck;
using AutumnBox.OpenFramework.Open.V1;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Internal
{

    /// <summary>
    /// 拓展的运行管理器
    /// </summary>
    public class ExtensionRuntime:Context
    {
        public override string Tag => $"{InnerExtension.Name}'s RT";
        public string Name { get { return InnerExtension.Name; } }
        public readonly AutumnBoxExtension InnerExtension;
        public bool IsRuning { get; private set; }
        private ExtensionRuntime(AutumnBoxExtension ext)
        {
            this.InnerExtension = ext;
        }
        [Hide]
        internal static ExtensionRuntime Create(Type type, InitArgs args = null)
        {
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
        [Hide]
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
                OpenApi.Log.Warn(this, "failed.....", ex);
                var wasFailedMsg = $"{Name} {OpenApi.Gui.GetPublicResouce<String>(this, "msgExtensionWasFailed")}";
                OpenApi.Gui.ShowMessageBox(this,"Warning", wasFailedMsg);
            }
            finally
            {
                IsRuning = false;
            }
        }
        [Hide]
        public bool Stop(StopArgs args = null)
        {
            if (!IsRuning) return true;
            return InnerExtension.OnStopCommand(args ?? new StopArgs());
        }
        [Hide]
        public void Destory(DestoryArgs args = null)
        {
            InnerExtension.OnDestory(args ?? new DestoryArgs());
        }
        [Hide]
        public void WaitForFinish()
        {
            while (IsRuning) ;
        }
    }
}
