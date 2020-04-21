using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.ManagedAdb
{
    /// <summary>
    /// 整个Basic程序集的管理器
    /// </summary>
    public static class BasicLoader
    {
        public IAdbManager AdbManager { get; set; }
    }
}
