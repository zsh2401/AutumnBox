/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 15:44:31 (UTC +8:00)
** desc： ...
*************************************************/
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutumnBox.Reporter.MVVM
{
    public class NotificationObject :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged([CallerMemberName]string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
