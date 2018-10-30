/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/30 22:12:59 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Service
{
    /// <summary>
    /// 服务ID
    /// </summary>
    public class ServiceName : Attribute
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name"></param>
        public ServiceName(string name)
        {
            this.Name = name;
        }
    }
}
