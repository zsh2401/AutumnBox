/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/27 0:10:33 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using System;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Internal
{

    /// <summary>
    /// 拓展的运行管理器
    /// </summary>
    public class ExtensionRuntime : Context
    {
        public override string Tag => $"{InnerExtension.Name}'s RT";
        public string Name { get { return InnerExtension.Name; } }
        public readonly AutumnBoxExtension InnerExtension;
        public bool IsRuning { get; private set; }
        private ExtensionRuntime(AutumnBoxExtension ext)
        {
            this.InnerExtension = ext;
        }
        internal static ExtensionRuntime Create(Type type, InitArgs args = null)
        {
            var ext = (AutumnBoxExtension)Activator.CreateInstance(type);
            if (!ext.InitAndCheck(args ?? new InitArgs())) throw new Exception("Cannot init");
            return new ExtensionRuntime(ext);
        }
        public async void RunAsync(Context ctx, StartArgs args, Action finishedCallback)
        {
            await Task.Run(() =>
            {
                Run(ctx, args);
            });
            finishedCallback();
        }
        public void Run(Context ctx, StartArgs args)
        {
            ctx.PermissionCheck(ContextPermissionLevel.Mid);
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
                OpenApi.Gui.ShowMessageBox(this, "Warning", wasFailedMsg);
            }
            finally
            {
                IsRuning = false;
            }
        }
        public bool Stop(Context ctx, StopArgs args = null)
        {
            try
            {
                ctx.PermissionCheck(ContextPermissionLevel.Mid);
                if (!IsRuning) return true;
                return InnerExtension.OnStopCommand(args ?? new StopArgs());
            }
            catch (Exception ex)
            {
                OpenApi.Log.Warn(this, "failed.....", ex);
                var wasFailedMsg = $"{Name} {OpenApi.Gui.GetPublicResouce<String>(this, "msgExtensionWasFailed")}";
                OpenApi.Gui.ShowMessageBox(this, "Warning", wasFailedMsg);
                return true;
            }
        }
        public void Destory(Context ctx, DestoryArgs args = null)
        {
            try
            {
                ctx.PermissionCheck(ContextPermissionLevel.Mid);
                InnerExtension.OnDestory(args ?? new DestoryArgs());
            }
            catch (Exception ex)
            {
                OpenApi.Log.Warn(this, "failed.....", ex);
            }
        }
        public void WaitForFinish()
        {
            while (IsRuning) ;
        }
    }
}
