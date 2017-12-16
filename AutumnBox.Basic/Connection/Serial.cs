using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Connection
{
    public class Serial
    {
        public bool IsIpAdress
        {
            get
            {
                return _ip != null;
            }
        }
        private IPEndPoint _ip = null;
        private string _serialNum = null;
        public override string ToString()
        {
            return _serialNum ?? _ip.ToString();
        }
        public string ToFullSerial() => $"-s {_serialNum ?? _ip.ToString()}";
        public Serial(string serialStr)
        {
            var strs = serialStr.Split(':');
            Logger.D("xxx" + serialStr);
            if (strs.Length > 0)
            {
                var ip = IPAddress.Parse(strs[0]);
                var port = Convert.ToInt32(strs[1]);
                _ip = new IPEndPoint(ip, port);
            }
            else
            {
                _serialNum = serialStr;
            }
        }
    }
}
