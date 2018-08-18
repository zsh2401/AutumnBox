/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/18 15:59:44 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Extension
{
    partial class AutumnBoxExtension
    {
		/// <summary>
        /// Ux是否存在
        /// </summary>
        protected virtual bool UxExistss => ExtensionUIController != null;
        /// <summary>
        /// 设置UI进度条
        /// </summary>
        protected virtual int ProgressValue
        {
            set
            {
                if (ExtensionUIController != null)
                {
                    ExtensionUIController.ProgressValue = value;
                }
            }
        }
        /// <summary>
        /// 设置简要信息
        /// </summary>
        protected virtual string Tip
        {
            set
            {
                WriteLine(value);
                if (ExtensionUIController != null)
                {
                    ExtensionUIController.Tip = value;
                }
            }
        }
        /// <summary>
        /// 写出一行数据
        /// </summary>
        /// <param name="msg"></param>
        protected virtual void WriteLine(string msg)
        {
            Logger.Info(msg);
            if (ExtensionUIController != null)
            {
                ExtensionUIController.AppendLine(msg);
            }
        }
    }
}
