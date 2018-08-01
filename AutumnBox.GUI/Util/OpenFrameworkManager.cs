/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:36:04 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.OpenApiImpl;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Open.Impl.AutumnBoxApi;

namespace AutumnBox.GUI.Util
{
    internal static class OpenFrameworkManager
    {
        public static void Init()
        {
            AutumnBoxGuiApiProvider.Inject(new AppManagerImpl());
            var instace = ExtensionManager.Instance;
        }
        ///// <summary>
        ///// GUIApi实现
        ///// </summary>
        //private class GuiApiImpl : IAppGuiManager
        //{
        //    public string CurrentLanguageCode => App.Current.Resources["LanguageCode"].ToString();

        //    public bool IsRunAsAdmin => SystemHelper.HaveAdminPermission;

        //    public Window GetMainWindow(Context context)
        //    {
        //        return App.Current.MainWindow;
        //    }

        //    public object GetPublicResouce(Context context, string key)
        //    {
        //        return App.Current.Resources[key];
        //    }

        //    public TReturn GetPublicResouce<TReturn>(Context context, string key) where TReturn : class
        //    {
        //        return App.Current.Resources[key] as TReturn;
        //    }

        //    public void RestartApp(Context ctx)
        //    {
        //        App.Current.Dispatcher.Invoke(() =>
        //        {
        //            var fmt = App.Current.Resources["msgRequestRestartAppFormat"].ToString();
        //            string msg = String.Format(fmt, ctx.GetType().Name);
        //            bool userIsAccept = BoxHelper.ShowChoiceDialog("Notice", msg, "btnDeny", "btnAccept").ToBool();
        //            if (userIsAccept)
        //            {
        //                Application.Current.Shutdown();
        //                Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
        //            }
        //            else
        //            {
        //                throw new UserDeniedException();
        //            }
        //        });
        //    }

        //    public void RunOnUIThread(Context context, Action act)
        //    {
        //        App.Current.Dispatcher.Invoke(act);
        //    }

        //    public bool? ShowChoiceBox(Context context, string title, string msg, string btnLeft = null, string btnRight = null)
        //    {
        //        bool? result = null;
        //        App.Current.Dispatcher.Invoke(() =>
        //        {
        //            result = BoxHelper.ShowChoiceDialog(title, msg, btnLeft, btnRight).ToBool();
        //        });
        //        return result;
        //    }

        //    public void ShowDebugWindow(Context ctx)
        //    {
        //        App.Current.Dispatcher.Invoke(() =>
        //        {
        //            new DebugWindow().Show();
        //        });
        //    }

        //    public void ShowLoadingWindow(Context context, ICompletable completable)
        //    {
        //        App.Current.Dispatcher.Invoke(() =>
        //        {
        //            BoxHelper.ShowLoadingDialog(completable);
        //        });

        //    }

        //    public void ShowMessageBox(Context context, string title, string msg)
        //    {
        //        App.Current.Dispatcher.Invoke(() =>
        //        {
        //            BoxHelper.ShowMessageDialog(title, msg);
        //        });
        //    }

        //    public void ShutdownApp(Context ctx)
        //    {
        //        App.Current.Dispatcher.Invoke(() =>
        //        {
        //            var fmt = App.Current.Resources["msgRequestShutdownAppFormat"].ToString();
        //            string msg = String.Format(fmt, ctx.GetType().Name);
        //            bool userIsAccept = BoxHelper.ShowChoiceDialog("Notice", msg, "btnDeny", "btnAccept").ToBool();
        //            if (userIsAccept)
        //            {
        //                Application.Current.Shutdown();
        //            }
        //            else
        //            {
        //                throw new UserDeniedException();
        //            }
        //        });
        //    }

        //    public void RefreshExtensionList(Context ctx)
        //    {
        //        App.Current.Dispatcher.Invoke(() =>
        //        {
        //            ThridPartyFunctionPanel.Single.Refresh();
        //        });
        //    }

        //    public void RestartAppAsAdmin(Context ctx)
        //    {
        //        App.Current.Dispatcher.Invoke(() =>
        //        {
        //            var fmt = App.Current.Resources["msgRequestRestartAppAsAdminFormat"].ToString();
        //            string msg = String.Format(fmt, ctx.GetType().Name);
        //            bool userIsAccept = BoxHelper.ShowChoiceDialog("Notice", msg, "btnDeny", "btnAccept").ToBool();
        //            if (userIsAccept)
        //            {
        //                ProcessStartInfo startInfo = new ProcessStartInfo( "..\\AutumnBox-秋之盒.exe");
        //                startInfo.Arguments = $"-tryadmin -waitatmb";
        //                Logger.Debug(this,startInfo.FileName + "  " +startInfo.Arguments);
        //                Process.Start(startInfo);
        //                Application.Current.Shutdown();
        //            }
        //            else
        //            {
        //                throw new UserDeniedException();
        //            }
        //        });
        //    }
        //}
        ///// <summary>
        ///// 调试API实现
        ///// </summary>
        //private class LogApiImpl : ILogApi
        //{
        //    public void Debug(Context sender, string msg)
        //    {
        //        Support.Log.Logger.Debug(sender.Tag, msg);
        //    }

        //    public void Info(Context sender, string msg)
        //    {
        //        Support.Log.Logger.Info(sender.Tag, msg);
        //    }

        //    public void Warn(Context sender, string msg)
        //    {
        //        Support.Log.Logger.Warn(sender.Tag, msg);
        //    }

        //    public void Warn(Context sender, string msg, Exception e)
        //    {
        //        Support.Log.Logger.Warn(sender.Tag, msg, e);
        //    }
        //}
    }
}
