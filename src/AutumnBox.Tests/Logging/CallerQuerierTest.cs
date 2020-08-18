/*

* ==============================================================================
*
* Filename: CallerQuerierTest
* Description: 
*
* Version: 1.0
* Created: 2020/8/18 10:14:24
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Logging;
using AutumnBox.Logging.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutumnBox.Tests.Logging
{
    [TestClass]
    public class CallerQuerierTest 
    {

        [TestMethod]
        public void NormalMethod()
        {
            Assert.AreEqual(nameof(CallerQuerierTest), CallerQuerier.GetCurrent().TypeName);
        }

        [TestMethod]
        public void ActionMethod()
        {
            Action a = () =>
            {
                Assert.AreEqual(nameof(CallerQuerierTest), CallerQuerier.GetCurrent().TypeName);
            };
            a();
            Action<int> b = (x) =>
            {
                a();
            };
            b(1);
        }

        [TestMethod]
        public void FuncMethod()
        {
            Func<int> a = () =>
            {
                Assert.AreEqual(nameof(CallerQuerierTest), CallerQuerier.GetCurrent().TypeName);
                return 1;
            };
            a();
            Func<int, int> b = (x) =>
             {
                 a();
                 return x;
             };
            b(1);
        }

        [TestMethod]
        public void Event()
        {
            TestEvent += (s, e) => Assert.AreEqual(nameof(CallerQuerierTest), CallerQuerier.GetCurrent().TypeName);
            TestEvent?.Invoke(this, new EventArgs());
        }

        [TestMethod]
        public void OtherClassTest()
        {
            var clazz = new OtherClass();
            clazz.Called += clazz.OtherClass_Called;
            clazz.Raise();
        }


        public event EventHandler? TestEvent;

        public class OtherClass : ICloneable
        {
            public event EventHandler? Called;

            public void Raise()
            {
                Called?.Invoke(this, new EventArgs());
            }

            public object Clone()
            {
                throw new NotImplementedException();
            }

            public void OtherClass_Called(object? sender, EventArgs e)
            {
                Assert.AreEqual(nameof(OtherClass), CallerQuerier.GetCurrent().TypeName);
            }
        }
    }
}
