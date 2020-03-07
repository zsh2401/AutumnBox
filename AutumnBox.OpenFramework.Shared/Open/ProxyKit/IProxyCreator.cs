using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open.ProxyKit
{
    /// <summary>
    /// 代理构建器
    /// </summary>
    public interface IProxyCreator
    {
        IProxy CreateProxyOf(Type type);
    }
}
