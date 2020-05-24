/*

* ==============================================================================
*
* Filename: FakeBaseApi
* Description: 
*
* Version: 1.0
* Created: 2020/5/24 16:55:30
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.Device;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open.LKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.OpenFX
{
    class FakeBaseApi : IBaseApi
    {
        public DirectoryInfo StorageDirectory => new DirectoryInfo(".");

        public DirectoryInfo TempDirectory => new DirectoryInfo(".");

        public IRegisterableLake GlobalLake
        {
            get
            {
                lake ??= new SunsetLake();
                return lake;
            }
        }
        private SunsetLake lake;

        public Version AutumnBoxLoggingVersion => new Version();

        public Version AutumnBoxGUIVersion => new Version();

        public Version AutumnBoxBasicVersion => new Version();

        public bool ShouldDisplayCmdWindow => false;

        public bool IsDeveloperMode => true;

        public bool IsRunAsAdmin => false;

        public Version Version => new Version();

        public IDevice SelectedDevice => null;

        public event EventHandler LanguageChanged;
        public event EventHandler Destorying;

        public void AddResource(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void AppendPanel(object view, int priority)
        {
            throw new NotImplementedException();
        }

        public void CloseLoadingUI()
        {
            throw new NotImplementedException();
        }

        public int DoChoice(string msg, string btnLeft = null, string btnRight = null, string btnCancel = null)
        {
            throw new NotImplementedException();
        }

        public bool DoYN(string message, string btnYes, string btnNo)
        {
            throw new NotImplementedException();
        }

        public string FakeLanguageCode { get; set; } = "zh-CN";
        public string GetCurrentLanguageCode() => FakeLanguageCode;

        public dynamic GetMainWindow()
        {
            throw new NotImplementedException();
        }

        public object GetNewView(string viewId)
        {
            throw new NotImplementedException();
        }

        public object GetResouce(string key)
        {
            throw new NotImplementedException();
        }

        public bool InputNumber(string hint, int min, int max, out int result)
        {
            throw new NotImplementedException();
        }

        public bool InputString(string hint, out string result)
        {
            throw new NotImplementedException();
        }

        public ILeafUI NewLeafUI()
        {
            throw new NotImplementedException();
        }

        public void PlayErr()
        {
            throw new NotImplementedException();
        }

        public void PlayOk()
        {
            throw new NotImplementedException();
        }

        public void RefreshExtensionList()
        {
            throw new NotImplementedException();
        }

        public void RemovePanel(object view)
        {
            throw new NotImplementedException();
        }

        public void Restart()
        {
            throw new NotImplementedException();
        }

        public void RestartAsAdmin()
        {
            throw new NotImplementedException();
        }

        public void RunOnUIThread(Action act)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendNotificationAsk(string msg)
        {
            throw new NotImplementedException();
        }

        public void SendNotificationInfo(string msg)
        {
            throw new NotImplementedException();
        }

        public void SendNotificationSuccess(string msg)
        {
            throw new NotImplementedException();
        }

        public void SendNotificationWarn(string msg)
        {
            throw new NotImplementedException();
        }

        public void SetResource(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void SetWindowBlur(IntPtr hWnd)
        {
            throw new NotImplementedException();
        }

        public void ShowDebugUI()
        {
            throw new NotImplementedException();
        }

        public void ShowException(string title, string sketch, string exceptionMessage)
        {
            throw new NotImplementedException();
        }

        public void ShowLoadingUI()
        {
            throw new NotImplementedException();
        }

        public void ShowMessage(string title, string msg)
        {
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        public object? UnstableInternalApiCall(string message, object? arg = null)
        {
            throw new NotImplementedException();
        }
    }
}
