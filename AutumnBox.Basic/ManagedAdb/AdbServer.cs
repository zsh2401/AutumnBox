/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 2:10:31 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ManagedAdb
{
    public class AdbServer : IAdbServer
    {
        public static IAdbServer Instance { get; private set; }

        public ushort Port => 5037;

        static AdbServer()
        {
            Instance = new AdbServer();
        }

        public bool AliveCheck()
        {
            throw new NotImplementedException();
        }

        public void Start(ushort port = 5037)
        {
            throw new NotImplementedException();
        }

        public void Restart()
        {
            throw new NotImplementedException();
        }

        public void Kill()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
