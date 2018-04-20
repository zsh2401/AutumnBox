/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:36:04 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.FlowFramework;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.UI.FuncPanels;
using AutumnBox.GUI.Windows;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Internal;
using AutumnBox.OpenFramework.Open;
using System;
using System.Diagnostics;
using System.Windows;

namespace AutumnBox.GUI.Util
{
    internal static class OpenFramewokManager
    {
        private class OpenFramewokManagerContext : Context { }
        public static void LoadApi()
        {
            var context = new OpenFramewokManagerContext();
            FrameworkLoader.SetGuiApi(context, new GuiApiImpl());
            FrameworkLoader.SetLogApi(context, new LogApiImpl());
        }
        /// <summary>
        /// GUIApi实现
        /// </summary>
        private class GuiApiImpl : IGuiApi
        {
            public string CurrentLanguageCode => App.Current.Resources["LanguageCode"].ToString();

            public bool IsRunAsAdmin => SystemHelper.HaveAdminPermission;

            public Window GetMainWindow(Context context)
            {
                return App.Current.MainWindow;
            }

            public object GetPublicResouce(Context context, string key)
            {
                return App.Current.Resources[key];
            }

            public TReturn GetPublicResouce<TReturn>(Context context, string key) where TReturn : class
            {
                return App.Current.Resources[key] as TReturn;
            }

            public void RestartApp(Context ctx)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    var fmt = App.Current.Resources["msgRequestRestartAppFormat"].ToString();
                    string msg = String.Format(fmt, ctx.GetType().Name);
                    bool userIsAccept = BoxHelper.ShowChoiceDialog("Notice", msg, "btnDeny", "btnAccept").ToBool();
                    if (userIsAccept)
                    {
                        Application.Current.Shutdown();
                        Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    }
                    else
                    {
                        throw new UserDeniedException();
                    }
                });
            }

            public void RunOnUIThread(Context context, Action act)
            {
                App.Current.Dispatcher.Invoke(act);
            }

            public bool? ShowChoiceBox(Context context, string title, string msg, string btnLeft = null, string btnRight = null)
            {
                bool? result = null;
                App.Current.Dispatcher.Invoke(() =>
                {
                    result = BoxHelper.ShowChoiceDialog(title, msg, btnLeft, btnRight).ToBool();
                });
                return result;
            }

            public void ShowDebugWindow(Context ctx)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    new DebugWindow().Show();
                });
            }

            public void ShowLoadingWindow(Context context, ICompletable completable)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    BoxHelper.ShowLoadingDialog(completable);
                });

            }

            public void ShowMessageBox(Context context, string title, string msg)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    BoxHelper.ShowMessageDialog(title, msg);
                });
            }

            public void ShutdownApp(Context ctx)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    var fmt = App.Current.Resources["msgRequestShutdownAppFormat"].ToString();
                    string msg = String.Format(fmt, ctx.GetType().Name);
                    bool userIsAccept = BoxHelper.ShowChoiceDialog("Notice", msg, "btnDeny", "btnAccept").ToBool();
                    if (userIsAccept)
                    {
                        Application.Current.Shutdown();
                    }
                    else
                    {
                        throw new UserDeniedException();
                    }
                });
            }
            public void RefreshExtensionList(Context ctx)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    ThridPartyFunctionPanel.Single.Refresh();
                });
            }
        }
        /// <summary>
        /// 调试API实现
        /// </summary>
        private class LogApiImpl : ILogApi
        {
            public void Debug(Context sender, string msg)
            {
                Support.Log.Logger.Debug(sender.Tag, msg);
            }

            public void Info(Context sender, string msg)
            {
                Support.Log.Logger.Info(sender.Tag, msg);
            }

            public void Warn(Context sender, string msg)
            {
                Support.Log.Logger.Warn(sender.Tag, msg);
            }

            public void Warn(Context sender, string msg, Exception e)
            {
                Support.Log.Logger.Warn(sender.Tag, msg, e);
            }
        }
    }
}
