using System.Collections.Generic;
using AutumnBox.OpenFramework.Extension.Leaf.Attributes;
using AutumnBox.OpenFramework.Extension.Leaf.Internal;
using AutumnBox.OpenFramework.LeafExtension.Internal;

namespace AutumnBox.OpenFramework.Extension.Leaf
{
    /// <summary>
    /// Leaf模块
    /// </summary>
    public abstract class LeafExtensionBase : EmptyExtension, IClassExtension
    {
        private readonly LSignalDistributor signalDistributor;
        private readonly LeafEntryExecutor executor;
        private readonly LeafPropertyInjector injector;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LeafExtensionBase()
        {
            //初始化API注入器
            ApiAllocator apiAllocator = new ApiAllocator(this);

            //注入属性
            injector = new LeafPropertyInjector(this, apiAllocator);
            injector.Inject();

            //构造入口点执行器
            executor = new LeafEntryExecutor(this, apiAllocator);

            //注册信号接收系统
            signalDistributor = new LSignalDistributor(this);
            signalDistributor.ScanReceiver();
        }

        /// <summary>
        /// 入口函数,继承者无需关心
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [LDoNotScan]
        public int Main(Dictionary<string, object> data)
        {
            return executor.Execute(data) ?? 0;
        }

        /// <summary>
        /// 接收信号
        /// </summary>
        /// <param name="signalName"></param>
        /// <param name="value"></param>
        public void ReceiveSignal(string signalName, object value = null)
        {
            signalDistributor.Receive(signalName, value);
        }
    }
}
