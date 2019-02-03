using AutumnBox.Basic.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    /// <summary>
    /// LeafExtension的一些小工具
    /// </summary>
    public class LeafKit
    {
        /// <summary>
        /// UI
        /// </summary>
        public ILeafUI UI { get; set; }
        /// <summary>
        /// 日志器
        /// </summary>
        public ILeafLogger Logger { get; set; }
        /// <summary>
        /// 写出到行
        /// </summary>
        /// <param name="content"></param>
        public void WriteLine(object content)
        {
            UI?.WriteLine(content);
            Logger?.Info(content);
        }
        /// <summary>
        /// 输出接收器
        /// </summary>
        public Action<OutputReceivedEventArgs> OutputReceiver { get => _lazyOutputReceiver.Value; }
        private Lazy<Action<OutputReceivedEventArgs>> _lazyOutputReceiver;
        /// <summary>
        /// 构造LeafKit实例
        /// </summary>
        public LeafKit()
        {
            _lazyOutputReceiver = new Lazy<Action<OutputReceivedEventArgs>>(() =>
            {
                return (e) => WriteLine(e.Text);
            });
        }
    }
}
