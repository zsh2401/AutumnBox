using AutumnBox.Leafx.Container.Support;
using HandyControl.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(INotificationManager))]
    sealed class NotificationManager : INotificationManager
    {
        private const int STATE_CHECK_INTERVAL = 300;

        public string Token { get; set; }

        private Task WaitForTokenLoaded()
        {
            return Task.Run(() =>
            {
                while (Token == null)
                {
                    Thread.Sleep(STATE_CHECK_INTERVAL);
                }
            });
        }

        public async void Info(string msg)
        {
            await WaitForTokenLoaded();
            if (Thread.CurrentThread != App.Current.Dispatcher.Thread)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    Growl.Info(msg, Token);
                });
            }
            else
            {
                Growl.Info(msg, Token);
            }
        }

        public async void Warn(string msg)
        {
            await WaitForTokenLoaded();
            if (Thread.CurrentThread != App.Current.Dispatcher.Thread)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    Growl.Warning(msg, Token);
                });
            }
            else
            {
                Growl.Warning(msg, Token);
            }
        }

        public Task<bool> Ask(string msg)
        {
            return Task.Run(() =>
             {
                 WaitForTokenLoaded();
                 bool? result = null;
                 App.Current.Dispatcher.Invoke(() =>
                 {
                     Growl.Ask(ParseMsg(msg), (_result) =>
                     {
                         result = _result;
                         return true;
                     }, Token);
                 });
                 while (result == null)
                 {
                     Thread.Sleep(STATE_CHECK_INTERVAL);
                 }
                 return result == true;
             });
        }

        public async void Success(string msg)
        {
            await WaitForTokenLoaded();
            if (Thread.CurrentThread != App.Current.Dispatcher.Thread)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    Growl.Success(ParseMsg(msg), Token);
                });
            }
            else
            {
                Growl.Success(msg, Token);
            }
        }

        private string ParseMsg(string msg)
        {
            return App.Current.Resources[msg]?.ToString() ?? msg;
        }
    }
}
