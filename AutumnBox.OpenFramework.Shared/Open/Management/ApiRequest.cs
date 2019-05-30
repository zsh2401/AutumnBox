using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open.Management
{
    /// <summary>
    /// 初始化设置
    /// </summary>
    public class ApiRequest
    {
        /// <summary>
        /// 此次API请求的唯一ID
        /// </summary>
        public Guid ApiGuid { get; }

        /// <summary>
        /// 要获取的API类型
        /// </summary>
        public Type TargetApiType { get; }

        /// <summary>
        /// API的申请者
        /// </summary>
        public object RequesterInstance { get; }

        /// <summary>
        /// requester的类型,默认返回Requester.GetType(),也可自行设置,通过由静态类使用
        /// </summary>
        public Type RequesterType { get; }

        /// <summary>
        /// 一些特定参数
        /// </summary>
        public object Arg { get; }

        /// <summary>
        /// 保护的API请求构造方法
        /// </summary>
        protected ApiRequest() { }
        /// <summary>
        /// 构造API请求
        /// </summary>
        /// <param name="targetApiType"></param>
        /// <param name="requester"></param>
        /// <param name="arg"></param>
        /// <param name="requesterType"></param>
        public ApiRequest(Type targetApiType, object requester = null, object arg = null, Type requesterType = null)
        {
            ApiGuid = Guid.NewGuid();
            if (targetApiType == null)
            {
                throw new ArgumentNullException(nameof(targetApiType));
            }
            TargetApiType = targetApiType;
            RequesterType = requesterType ?? requester.GetType();
            RequesterInstance = requester;
            Arg = arg;
        }
    }
}
