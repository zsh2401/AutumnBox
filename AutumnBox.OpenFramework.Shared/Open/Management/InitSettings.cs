using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open.Management
{
    /// <summary>
    /// 初始化设置
    /// </summary>
    public class InitSettings
    {
        /// <summary>
        /// 此次API请求的唯一ID
        /// </summary>
        public Guid ApiGuid { get; }
        /// <summary>
        /// API的申请者
        /// </summary>
        public object Requester { get; }
        /// <summary>
        /// 一些特定参数
        /// </summary>
        public object Arg { get; }
        /// <summary>
        /// 构造API初始化设置器
        /// </summary>
        /// <param name="requester"></param>
        /// <param name="arg"></param>
        public InitSettings(object requester, object arg)
        {
            Requester = requester;
            Arg = arg;
        }
    }
}
