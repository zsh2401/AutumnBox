/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 15:44:31 (UTC +8:00)
** desc： ...
*************************************************/
using System.ComponentModel;

namespace AutumnBox.GUI.UI
{
    class NotificationObject : InjectByInterfaceObject,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
