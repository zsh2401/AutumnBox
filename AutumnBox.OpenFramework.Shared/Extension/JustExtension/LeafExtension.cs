using System.Collections.Generic;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Wrapper;

namespace AutumnBox.OpenFramework.Extension.JustExtension
{
    public abstract class LeafExtension : IClassExtension
    {
        private class LeafExtensionContext : Context { }
        private readonly LeafExtensionContext context = new LeafExtensionContext();
        /// <summary>
        /// 返回码
        /// </summary>
        private int ExitCode { get; set; } = 0;
        private Dictionary<string, object> data;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LeafExtension()
        {
            var scanner = new ClassExtensionScanner(GetType());
            scanner.Scan(ClassExtensionScanner.ScanOption.BeforeCreatingAspect);
            var bcAspects = scanner.BeforeCreatingAspects;
            bool canContinue = true;
            BeforeCreatingAspectArgs bcaArgs = new BeforeCreatingAspectArgs(context, GetType());
            foreach (var aspect in bcAspects)
            {
                aspect.BeforeCreating(bcaArgs, ref canContinue);
                if (!canContinue)
                {
                    throw new ClassExtensionBase.AspectPreventedException();
                }
            }
            RegisterSinalReceiver();
        }

        /// <summary>
        /// 入口函数,继承者无需关心
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Main(Dictionary<string, object> data)
        {
            this.data = data;
            RegisterSinalReceiver();
            InjectProperty();

            FindAndExecuteEntryPoint();
            return ExitCode;
        }

        private void InjectProperty() { }
        private void FindAndExecuteEntryPoint() { }
        private void RegisterSinalReceiver() { }

        /// <summary>
        /// 接收信号
        /// </summary>
        /// <param name="signalName"></param>
        /// <param name="value"></param>
        public void ReceiveSignal(string signalName, object value = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
