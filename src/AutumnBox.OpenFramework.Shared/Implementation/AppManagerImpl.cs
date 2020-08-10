/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 1:53:51 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Leafx.Container.Support;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutumnBox.OpenFramework.Implementation
{
    [Component(Type = typeof(IAppManager))]
    internal partial class AppManagerImpl : IAppManager
    {
        private readonly IBaseApi sourceApi;
        public AppManagerImpl(IBaseApi baseAPI)
        {
            this.sourceApi = baseAPI;
        }

        public bool IsRunAsAdmin
        {
            get
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    return sourceApi.IsRunAsAdmin;
                }
                else
                {
                    return false;
                }
            }
        }

        public string CurrentLanguageCode => sourceApi.GetCurrentLanguageCode();

        public Version Version => sourceApi.Version;

        public bool IsDeveloperMode => sourceApi.IsDeveloperMode;

        public bool ShouldDisplayCmdWindow => sourceApi.ShouldDisplayCmdWindow;

        /// <summary>
        /// 尚未实现
        /// </summary>
        public bool EnableAD => true;

        public dynamic GetMainWindow()
        {
            return sourceApi.GetMainWindow();
        }

        public object GetPublicResouce(string key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return sourceApi.GetResouce(key);
        }

        public TReturn GetPublicResouce<TReturn>(string key) where TReturn : class
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return sourceApi.GetResouce(key) as TReturn;
        }

        public void OpenUrl(string url)
        {
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

        public void RefreshExtensionView()
        {
            sourceApi.RunOnUIThread(() =>
            {
                sourceApi.RefreshExtensionList();
            });
        }

        public void RestartApp()
        {
            sourceApi.Restart();
            //if (ctx.Permission == CtxPer.High)
            //{
            //    sourceApi.Restart();
            //    return;
            //}
            //var fmtMsg = GetPublicResouce<string>("msgRequestRestartAppFormat");
            //string title = GetPublicResouce<string>("Notice");
            //string msg = string.Format(fmtMsg, ctx.GetType().Name);
            //if (ctx.Ux.DoChoice(msg, "btnDeny", "btnAccept") == ChoiceResult.Right)
            //{
            //    sourceApi.Restart();
            //}
            //else
            //{
            //    throw new UserDeniedException();
            //}
        }

        public void RestartAppAsAdmin()
        {
            sourceApi.RestartAsAdmin();
            //if (ctx.Permission == CtxPer.High)
            //{
            //    sourceApi.RestartAsAdmin();
            //    return;
            //}
            //var fmtMsg = GetPublicResouce<string>("msgRequestRestartAppAsAdminFormat");
            //string title = GetPublicResouce<string>("Notice");
            //string msg = string.Format(fmtMsg, ctx.GetType().Name);
            //if (ctx.Ux.DoChoice(msg, "btnDeny", "btnAccept") == ChoiceResult.Right)
            //{
            //    sourceApi.RestartAsAdmin();
            //}
            //else
            //{
            //    throw new UserDeniedException();
            //}
        }

        public void RunOnUIThread(Action act)
        {
            if (act is null)
            {
                throw new ArgumentNullException(nameof(act));
            }

            sourceApi.RunOnUIThread(act);
        }

        public void ShowException(string title, string sketch, Exception e)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("message", nameof(title));
            }

            if (string.IsNullOrEmpty(sketch))
            {
                throw new ArgumentException("message", nameof(sketch));
            }

            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }
            sourceApi.RunOnUIThread(() =>
            {
                sourceApi.ShowException(title, sketch, e.ToString());
            });
        }

        public void ShutdownApp()
        {
            sourceApi.Shutdown();
        }
    }
}
