/*

* ==============================================================================
*
* Filename: MOTDHandler
* Description: 
*
* Version: 1.0
* Created: 2020/3/16 23:46:37
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.GUI.Util.Bus;
using AutumnBox.Logging;
using HandyControl.Controls;
using HandyControl.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Remote
{
    class MOTDHandler
    {
        private class Result
        {
            /// <summary>
            /// 内容
            /// </summary>
            [JsonProperty("msg")]
            public string Message { get; set; }
            /// <summary>
            /// 点击跳转的地址
            /// </summary>
            [JsonProperty("clickUrl")]
            public string ClickUrl { get; set; }

            /// <summary>
            /// 点击跳转的地址
            /// </summary>
            [JsonProperty("goBtnStr")]
            public string GoBtnStr { get; set; }
        }
        public async Task Do(APIIndex index)
        {
            var result = await new NetReader().Read(index.MotdUrl)
                .ContinueWith(task => task.Result.ToJson<Result>());
            SLogger<MOTDHandler>.Info(result.Message);
            if (result.ClickUrl != null)
            {
                Growl.Ask(new GrowlInfo()
                {
                    Message = result.Message,
                    ShowCloseButton = true,
                    ConfirmStr = result.GoBtnStr ?? "Go",
                    Token = MainWindowBus.TOKEN_PANEL_MAIN
                });
            }
            else
            {
                MainWindowBus.Info(result.Message);
            }
        }
    }
}
