using AutumnBox.Basic;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.Tests.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace AutumnBox.Tests.Basic.Devices
{
    [TestClass]
    public class DevicesTest : IDisposable
    {
        public DevicesTest()
        {
            BasicBooter.Use<Win32AdbManager>();
        }
        public void Dispose()
        {
            BasicBooter.Free();
        }

        [TestMethod]
        public void Test()
        {
            var getter = new DevicesGetter();
            getter.GetDevices().All((dev) =>
            {
                Debug.WriteLine(dev);
                return true;
            }); ;
        }
    }
}
