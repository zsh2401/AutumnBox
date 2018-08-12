/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 1:53:51 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.OpenFramework.Open.Impl.AutumnBoxApi;
using System;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.OpenFramework.Open.Impl
{
    internal partial class AppManagerImpl : IAppManager
    {
        private readonly IAutumnBoxGuiApi sourceApi;
        private readonly Context ctx;
        public AppManagerImpl(Context ctx, IAutumnBoxGuiApi sourceApi)
        {
            this.ctx = ctx;
            this.sourceApi = sourceApi;
        }

        public bool IsRunAsAdmin
        {
            get
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public string CurrentLanguageCode => sourceApi.GetCurrentLanguageCode();

        public Version Version => sourceApi.Version;

        public void CloseLoadingWindow()
        {
            RunOnUIThread(() =>
            {
                sourceApi.CloseLoadingWindow();
            });
        }

        public Window CreateDebuggingWindow()
        {
            return sourceApi.CreateDebugWindow();
        }

        public Window GetMainWindow()
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

        public void RefreshExtensionList()
        {
            sourceApi.RefreshExtensionList();
        }

        public void RestartApp()
        {
            var fmtMsg = GetPublicResouce<string>("msgRequestRestartAppFormat");
            string title = GetPublicResouce<string>("Notice");
            string msg = string.Format(fmtMsg, ctx.GetType().Name);
            if (ShowChoiceBox(title, msg, "btnDeny", "btnAccept") == ChoiceBoxResult.Right)
            {
                sourceApi.Restart();
            }
            else
            {
                throw new UserDeniedException();
            }
        }

        public void RestartAppAsAdmin()
        {
            var fmtMsg = GetPublicResouce<string>("msgRequestRestartAppAsAdminFormat");
            string title = GetPublicResouce<string>("Notice");
            string msg = string.Format(fmtMsg, ctx.GetType().Name);
            if (ShowChoiceBox(title, msg, "btnDeny", "btnAccept") == ChoiceBoxResult.Right)
            {
                sourceApi.RestartAsAdmin();
            }
            else
            {
                throw new UserDeniedException();
            }
        }

        public void RunOnUIThread(Action act)
        {
            sourceApi.RunOnUIThread(act);
        }

        public ChoiceBoxResult ShowChoiceBox(string title, string msg, string btnLeft = null, string btnRight = null)
        {
            return sourceApi.ShowChoiceBox(title, msg, btnLeft, btnRight);
        }

        public void ShowLoadingWindow()
        {
            Task.Run(() =>
            {
                RunOnUIThread(() =>
                {
                    sourceApi.ShowLoadingWindow();
                });
            });
        }

        public void ShowMessageBox(string title, string msg)
        {
            sourceApi.ShowMessageBox(title, msg);
        }

        public void ShutdownApp()
        {
            var fmtMsg = GetPublicResouce<string>("msgRequestShutdownAppFormat");
            string title = GetPublicResouce<string>("Notice");
            string msg = string.Format(fmtMsg, ctx.GetType().Name);
            if (ShowChoiceBox(title, msg, "btnDeny", "btnAccept") == ChoiceBoxResult.Right)
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
