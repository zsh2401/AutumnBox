/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 16:21:56 (UTC +8:00)
** desc： ...
*************************************************/

using System.Runtime.CompilerServices;

namespace AutumnBox.GUI.MVVM
{
    class ViewModelBase : NotificationObject
    {
        protected virtual bool RaisePropertyChangedOnDispatcher { get; set; } = false;
        protected override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (RaisePropertyChangedOnDispatcher)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    base.RaisePropertyChanged(propertyName);
                });
            }
            else
            {
                base.RaisePropertyChanged(propertyName);
            }

        }
    }
}
