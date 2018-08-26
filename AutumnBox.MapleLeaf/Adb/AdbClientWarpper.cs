/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 2:16:08 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Adb
{
    public class AdbClientWarpper
    {
        private readonly IAdbClient core;
        public AdbClientWarpper(IAdbClient core)
        {
            this.core = core;
        }
        public AdbResponse SetDevice(string serialNumber)
        {
            return SendRequest($"host:transport:{serialNumber}", false);
        }
        public AdbResponse SendRequest(string request, bool readDataWhenOkay = true)
        {
            core.SendRequest(request);
            string stateString = core.ReceiveState();
            AdbResponse response = new AdbResponse()
            {
                IsOkay = stateString == AdbProtocol.STATE_OKAY,
            };
            if (readDataWhenOkay || !response.IsOkay)
            {
                response.Data = core.ReceiveData();
            }
            return response;
        }
    }
}
