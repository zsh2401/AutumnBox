/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 16:21:56 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.Util.Debugging;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AutumnBox.GUI.MVVM
{
    class ViewModelBase : NotificationObject
    {
        public ICommand OpenUrl
        {
            get
            {
                return _openUrl;
            }
            set
            {
                _openUrl = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _openUrl;

        public ICommand OpenGoUrl
        {
            get
            {
                return _openGoUrl;
            }
            set
            {
                _openGoUrl = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _openGoUrl;

        public ViewModelBase()
        {
            OpenUrl = new FlexiableCommand(_OpenUrl);
            OpenGoUrl = new FlexiableCommand(_OpenGoUrl);
        }

        protected void _OpenGoUrl(object para)
        {
            SLogger.Info(this, para);
            var goPre = App.Current.Resources["UrlGoPrefix"] as string;
            try
            {
                Process.Start(goPre + para);
            }
            catch (Exception e)
            {
                SLogger.Warn(this, $"can not open url {para}", e);
            }
        }
        protected void _OpenUrl(object para)
        {
            try
            {
                Process.Start(para as string);
            }
            catch (Exception e)
            {
                SLogger.Warn(this, $"can not open url {para}", e);
            }
        }

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
