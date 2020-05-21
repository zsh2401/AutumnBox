using AutumnBox.Basic;
using AutumnBox.Basic.MultipleDevices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace AutumnBox.Tests.Basic.Devices
{
    [TestClass]
    public class DevicesTest
    {
        [TestMethod]
        public void Test()
        {
            //BasicBooter.Use<Win32AdbManager>();
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
