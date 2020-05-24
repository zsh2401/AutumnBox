using AutumnBox.Basic;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.Tests.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace AutumnBox.Tests.Basic.Devices
{
    [TestClass]
    public class DevicesTest
    {
        public DevicesTest()
        {
            BasicBooter.Use<Win32AdbManager>();
        }
        ~DevicesTest()
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
            BasicBooter.Free();
        }
    }
}
