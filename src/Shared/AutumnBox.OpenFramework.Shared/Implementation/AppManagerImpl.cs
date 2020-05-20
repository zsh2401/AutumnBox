/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 1:53:51 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Leafx.Container.Support;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using System;

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
