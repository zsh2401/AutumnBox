/* =============================================================================*\
*
* Filename: Stantisticians
* Description: 
*
* Version: 1.0
* Created: 2017/11/28 15:24:53 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.GUI.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.NetUtil
{
    public struct DataLayout {
        [JsonProperty("mac")]
        public string Mac { get; set; }
        [JsonProperty("windowsVer")]
        public string Version { get;  }
    }
    public class Stantisticians : DataSender
    {
        MachineInfo MI = new MachineInfo();
        protected override string Address => "192.186.0.5";

        protected override string Mac => MI.GetLocatMac();

        protected override string Datas => GetTheDatas();
        public Stantisticians() {
            MachineInfo MI = new MachineInfo();
        }
        private string GetTheDatas() {
            throw new Exception();
        }
    }
}
