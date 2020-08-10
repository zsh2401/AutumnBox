/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 16:21:56 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.Services;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace AutumnBox.GUI.MVVM
{
    /// <summary>
    /// 表示ViewModel基础类
    /// </summary>
    internal class ViewModelBase : NotificationObject
    {
        public ICommand ShowWindowDialog
        {
            get => _showWindowDialog; set
            {
                _showWindowDialog = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _showWindowDialog;

        public ICommand ShowWindow
        {
            get => _showWindow; set
            {
                _showWindow = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _showWindow;

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

        public ICommand RefreshHomeContent
        {
            get
            {
                return _refreshHomeContent;
            }
            set
            {
                _refreshHomeContent = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _refreshHomeContent;

        [AutoInject]
        private ISentenceService SentenceService { get; set; }
        public string Sentence => SentenceService.Next();

        public ViewModelBase()
        {
            OpenUrl = _OpenUrlCommand;
            OpenGoUrl = _OpenGoUrlCommand;
            ShowWindowDialog = _ShowWindowDialogCommnand;
            ShowWindow = _ShowWindowCommand;
        }

        protected virtual bool RaisePropertyChangedOnUIThread { get; set; } = false;
        protected override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (RaisePropertyChangedOnUIThread)
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



        private static readonly ICommand _OpenGoUrlCommand;
        private static readonly ICommand _OpenUrlCommand;
        private static readonly ICommand _ShowWindowDialogCommnand;
        private static readonly ICommand _ShowWindowCommand;
        static ViewModelBase()
        {
            _OpenUrlCommand = new MVVMCommand(_OpenUrl);
            _OpenGoUrlCommand = new MVVMCommand(_OpenGoUrl);
            _ShowWindowDialogCommnand = new MVVMCommand(_ShowWindowDialog);
            _ShowWindowCommand = new MVVMCommand(_ShowWindow);
        }
        private static void _OpenGoUrl(object para)
        {
            var goPre = App.Current.Resources["UrlGoPrefix"] as string;
            try
            {
                _OpenUrl(goPre + para);
            }
            catch (Exception e)
            {
                SLogger<ViewModelBase>.Warn($"can not open url {para}", e);
            }
        }
        private static void _OpenUrl(object para)
        {
            string url = para as string;
            if (url == null) return;
            //https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
        private static void _ShowWindowDialog(object para)
        {
            App.Current.Lake.Get<IWindowManager>().ShowDialog(para.ToString());
        }
        private static void _ShowWindow(object para)
        {
            App.Current.Lake.Get<IWindowManager>().Show(para.ToString());
        }
    }
}
