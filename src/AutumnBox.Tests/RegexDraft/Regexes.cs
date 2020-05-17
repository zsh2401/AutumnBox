/*

* ==============================================================================
*
* Filename: Regexes
* Description: 
*
* Version: 1.0
* Created: 2020/5/17 12:55:59
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutumnBox.Tests.RegexDraft
{
    [TestClass]
    public class Regexes
    {
        [TestMethod]
        public void Test()
        {
            Regex regex = new Regex(@"^(?<ip>\d{0,3}\.\d{1,3}\.\d{0,3}\.\d{0,3}):{0,1}(?<port>\d{0,5})$");

            {
                var match = regex.Match("192.168.0.1:5555");
                Assert.IsTrue(match.Result("${ip}") == "192.168.0.1");
                Assert.IsTrue(match.Result("${port}") == "5555");
            }
            {
                var match = regex.Match("192.168.0.1:");
                Assert.IsTrue(match.Result("${ip}") == "192.168.0.1");
                Assert.IsTrue(match.Result("${port}") == "");
            }
            {
                var match = regex.Match("192.168.0.1");
                Assert.IsTrue(match.Result("${ip}") == "192.168.0.1");
                Assert.IsTrue(match.Result("${port}") == "");
            }
            {
                var match = regex.Match("192.168.0");
                Assert.IsFalse(match.Success);
            }
        }
    }
}
