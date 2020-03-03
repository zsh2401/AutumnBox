/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/23 19:13:03 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using System;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Implementation
{
    class UxImpl : IUx
    {
        private readonly IBaseApi sourceApi;

        public UxImpl(IBaseApi baseApi)
        {
            this.sourceApi = baseApi;
        }

        public bool DoYN(string message, string btnYes = null, string btnNo = null)
        {
            bool? result = false;
            RunOnUIThread(() =>
            {
                result = sourceApi.DoYN(message, btnYes, btnNo);
            });
            return result == true;
        }

        public ChoiceResult DoChoice(string message, string btnLeft = null, string btnRight = null, string btnCancel = null)
        {
            ChoiceResult result = ChoiceResult.Cancel;
            RunOnUIThread(() =>
            {
                int clickedBtnCode = sourceApi.DoChoice(message, btnLeft, btnRight, btnCancel);
                switch (clickedBtnCode)
                {
                    case 1:
                        result = ChoiceResult.Left;
                        break;
                    case 2:
                        result = ChoiceResult.Right;
                        break;
                    default:
                        result = ChoiceResult.Cancel;
                        break;
                }
            });
            return result;
        }


        public void ShowDebuggingWindow()
        {
            RunOnUIThread(() =>
            {
                sourceApi.ShowDebugUI();
            });
        }


        public void ShowLoadingWindow()
        {
            Task.Run(() =>
            {
                sourceApi.RunOnUIThread(() =>
                {
                    sourceApi.ShowLoadingUI();
                });
            });
        }

        public void CloseLoadingWindow()
        {
            sourceApi.RunOnUIThread(() =>
            {
                sourceApi.CloseLoadingUI();
            });
        }

        public void Message(string title, string message)
        {
            RunOnUIThread(() =>
            {
                sourceApi.ShowMessage(title, message);
            });
        }

        public void Message(string message)
        {
            RunOnUIThread(() =>
            {
                sourceApi.ShowMessage(sourceApi.GetResouce("OpenFxTitleMessage") as string, message);
            });
        }

        public void Warn(string message)
        {
            RunOnUIThread(() =>
            {
                sourceApi.ShowMessage(sourceApi.GetResouce("OpenFxTitleWarning") as string, message);
            });
        }

        public void Error(string message)
        {
            RunOnUIThread(() =>
            {
                sourceApi.ShowMessage(sourceApi.GetResouce("OpenFxTitleError") as string, message);
            });
        }

        public void RunOnUIThread(Action action)
        {
            sourceApi.RunOnUIThread(action);
        }

        public bool Agree(string message)
        {
            bool result = true;
            RunOnUIThread(() =>
            {
                result = DoChoice(message, "ChoiceWindowBtnDisagree", "ChoiceWindowBtnAgree", "ChoiceWindowBtnCancel") == ChoiceResult.Accept;
            });
            return result;
        }

        public int InputNumber(string hint = null, int min = int.MinValue, int max = int.MaxValue)
        {
            bool success = false;
            int result = 0;
            RunOnUIThread(() =>
            {
                success = sourceApi.InputNumber(hint, min, max, out result);
            });
            if (success)
            {
                return result;
            }
            else
            {
                throw new Exceptions.UserDeniedException();
            }
        }

        public string InputString(string hint)
        {
            bool success = false;
            string result = null;
            RunOnUIThread(() =>
            {
                success = sourceApi.InputString(hint, out result);
            });
            if (success)
            {
                return result;
            }
            else
            {
                throw new Exceptions.UserDeniedException();
            }
        }
    }
}
