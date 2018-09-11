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
    public enum ChoiceResult
    {
        /// <summary>
        /// 取消
        /// </summary>
        Cancel = -1,
        /// <summary>
        /// 左按,通常代表拒绝
        /// </summary>
        Left = 0,
        /// <summary>
        /// 右按钮,默认代表同意
        /// </summary>
        Right = 1,
        /// <summary>
        /// 默认情况的拒绝数值,等于Left
        /// </summary>
        Deny = Left,
        /// <summary>
        /// 默认情况的同意数值,等于Right
        /// </summary>
        Accept = Right,
    }
}
