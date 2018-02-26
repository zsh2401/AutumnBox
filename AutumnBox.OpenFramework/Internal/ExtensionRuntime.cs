/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/27 0:10:33 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Internal
{
    public class ExtensionRuntime
    {
        public string Name { get { return InnerExtension.Name; } }
        public readonly AutumnBoxExtension InnerExtension;
        public bool IsRuning { get; private set; }
        public ExtensionRuntime(AutumnBoxExtension ext)
        {
            this.InnerExtension = ext;
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
            InnerExtension.OnStartCommand(args);
            IsRuning = false;
            InnerExtension.OnFinished();
        }
        public bool Stop()
        {
            if (!IsRuning) return true;
            return InnerExtension.OnStopCommand();
        }
        public void Destory()
        {
            InnerExtension.OnDestory(new DestoryArgs());
        }
        public void WaitForFinish()
        {
            while (IsRuning) ;
        }
    }
}
