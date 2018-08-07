/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 3:09:36 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// App.ShowChoiceBox()的返回值
    /// </summary>
    public enum ChoiceBoxResult
    {
        /// <summary>
        /// 左按钮,通常代表同意
        /// </summary>
        Left = 0,
        /// <summary>
        /// 右按钮
        /// </summary>
        Right,
        /// <summary>
        /// 取消
        /// </summary>
        Cancel,
    }
}
