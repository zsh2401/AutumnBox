/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/23 19:13:03 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Warpper;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.OpenFramework.Open.Impl
{
    class UxImpl : IUx
    {
        private readonly IAutumnBox_GUI sourceApi;
        private readonly Context ctx;

        public UxImpl(Context ctx, IAutumnBox_GUI sourceApi)
        {
            this.ctx = ctx;
            this.sourceApi = sourceApi;
        }

        public ChoiceResult DoChoice(string message, string btnLeft = null, string btnRight = null, string btnCancel = null)
        {
            var window = sourceApi.CreateChoiceWindow(message, btnLeft, btnRight, btnCancel);
            window.ShowDialog();
            switch (window.DialogResult)
            {
                case true:
                    return ChoiceResult.Left;
                case false:
                    return ChoiceResult.Right;
                default:
                    return ChoiceResult.Cancel;
            }
        }

        public IExtensionUIController GetUIControllerOf(IExtensionWarpper warpper)
        {
            return sourceApi.GetUIControllerOf(warpper);
        }

        public void ShowDebuggingWindow()
        {
            sourceApi.CreateDebugWindow().Show();
        }

        private Window currentLoadingWindow;

        public void ShowLoadingWindow()
        {
            if (currentLoadingWindow != null)
            {
                throw new System.Exception("you can show just only one loading window");
            }
            Task.Run(() =>
            {
                ctx.App.RunOnUIThread(() =>
                {
                    currentLoadingWindow = sourceApi.CreateLoadingWindow();
                    currentLoadingWindow.ShowDialog();
                });
            });
        }

        public void CloseLoadingWindow()
        {
            ctx.App.RunOnUIThread(() =>
            {
                currentLoadingWindow.Close();
                currentLoadingWindow = null;
            });
        }

        public void ShowMessageDialog(string title, string message)
        {
            sourceApi.CreateMessageWindow(title, message).ShowDialog();
        }

        public void ShowMessageDialog(string message)
        {
            sourceApi.CreateMessageWindow(ctx.App.GetPublicResouce<string>("Notice"), message).ShowDialog();
        }

        public void ShowWarnDialog(string message)
        {
            sourceApi.CreateMessageWindow(ctx.App.GetPublicResouce<string>("Warning"), message).ShowDialog();
        }

        public void RunOnUIThread(Action action)
        {
            ctx.App.RunOnUIThread(action);
        }
    }
}
