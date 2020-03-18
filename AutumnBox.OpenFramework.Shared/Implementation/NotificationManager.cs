/*

* ==============================================================================
*
* Filename: NotificationManager
* Description: 
*
* Version: 1.0
* Created: 2020/3/17 0:31:29
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.ProxyKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Implementation
{
    class NotificationManager : INotificationManager
    {
        [Inject]
        public IBaseApi BaseApi { get; set; }

        public void SendNotification(string msg, string title = null, Action onClickHandler = null)
        {
            BaseApi.RunOnUIThread(() =>
            {
                BaseApi.SendNotification(msg, title, onClickHandler);
            });
        }
    }
}
