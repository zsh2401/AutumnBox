/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 15:44:31 (UTC +8:00)
** desc： ...
*************************************************/
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutumnBox.GUI.MVVM
{

    /// <summary>
    /// 表示可通知属性变化的类
    /// </summary>
    public class NotificationObject : InjectableObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
