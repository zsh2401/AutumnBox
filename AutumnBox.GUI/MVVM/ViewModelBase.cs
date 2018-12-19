/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 16:21:56 (UTC +8:00)
** desc： ...
*************************************************/

using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AutumnBox.GUI.MVVM
{
    class ViewModelBase : NotificationObject
    {
        //public ICommand OpenUrl => _lazyOpenUrl.Value;
        //private Lazy<ICommand> _lazyOpenUrl = 
        //    new Lazy<ICommand>(() => new OpenParameterUrlCommand());

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
