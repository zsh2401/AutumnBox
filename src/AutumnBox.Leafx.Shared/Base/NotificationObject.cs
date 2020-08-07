/*

* ==============================================================================
*
* Filename: NotificationObject
* Description: 
*
* Version: 1.0
* Created: 2020/5/7 1:46:49
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutumnBox.Leafx.Base
{
    /// <summary>
    /// 可通知属性变更的类
    /// </summary>
    public class NotificationObject : INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 触发属性变更事件
        /// </summary>
        /// <param name="memberName"></param>
        protected virtual void RaisePropertyChanged([CallerMemberName]string? memberName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }
    }
}
