/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/23 19:13:03 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Threading.Tasks;

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
            ChoiceResult result = ChoiceResult.Cancel;
            RunOnUIThread(() =>
            {
                dynamic window = sourceApi.CreateChoiceWindow(message, btnLeft, btnRight, btnCancel);
                window.ShowDialog();
                switch (window.DialogResult)
                {
                    case true:
                        result = ChoiceResult.Right;
                        break;
                    case false:
                        result = ChoiceResult.Left;
                        break;
                    default:
                        result = ChoiceResult.Cancel;
                        break;
                }
            });
            return result;
        }

        public IExtensionUIController GetUIControllerOf(IExtensionWrapper wrapper)
        {
            IExtensionUIController result = null;
            RunOnUIThread(() =>
            {
                result = sourceApi.GetUIControllerOf(wrapper);
            });
            return result;
        }

        public void ShowDebuggingWindow()
        {
            RunOnUIThread(() =>
            {
                sourceApi.CreateDebugWindow().Show();
            });
        }

        private dynamic currentLoadingWindow;

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
            RunOnUIThread(() =>
            {
                sourceApi.CreateMessageWindow(title, message).ShowDialog();
            });
        }

        public void ShowMessageDialog(string message)
        {
            RunOnUIThread(() =>
            {
                sourceApi.CreateMessageWindow(ctx.App.GetPublicResouce<string>("Notice"), message).ShowDialog();
            });
        }

        public void ShowWarnDialog(string message)
        {
            RunOnUIThread(() =>
            {
                sourceApi.CreateMessageWindow(ctx.App.GetPublicResouce<string>("Warning"), message).ShowDialog();
            });
        }

        public void RunOnUIThread(Action action)
        {
            ctx.App.RunOnUIThread(action);
        }

        public bool Agree(string message)
        {
            bool result = true;
            RunOnUIThread(() =>
            {
                result = DoChoice(message, "ChoiceWindowBtnDisagree", "ChoiceWindowBtnAgree", "ChoiceWindowBtnCancel") == ChoiceResult.Right;
            });
            return result;
        }
    }
}
