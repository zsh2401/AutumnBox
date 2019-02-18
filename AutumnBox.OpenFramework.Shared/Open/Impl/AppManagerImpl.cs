/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 1:53:51 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Security.Principal;
using System.Windows;

namespace AutumnBox.OpenFramework.Open.Impl
{
    internal partial class AppManagerImpl : IAppManager
    {
        private readonly IBaseApi sourceApi;
        private readonly Context ctx;
        public AppManagerImpl(Context ctx, IBaseApi sourceApi)
        {
            this.ctx = ctx;
            this.sourceApi = sourceApi;
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
            return sourceApi.GetResouce(key);
        }

        public TReturn GetPublicResouce<TReturn>(string key) where TReturn : class
        {
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
            sourceApi.RunOnUIThread(act);
        }

        public void ShutdownApp()
        {
            if (ctx.Permission == CtxPer.High)
            {
                sourceApi.Shutdown();
                return;
            }
            var fmtMsg = GetPublicResouce<string>("msgRequestShutdownAppFormat");
            string msg = string.Format(fmtMsg, ctx.GetType().Name);
            if (ctx.Ux.DoChoice(msg, "btnDeny", "btnAccept") == ChoiceResult.Right)
            {
                sourceApi.Shutdown();
            }
            else
            {
                throw new UserDeniedException();
            }
        }
    }
}
