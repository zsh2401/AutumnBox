using AutumnBox.Leafx.Enhancement.ClassTextKit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AutumnBox.Tests.Leafx
{
    [TestClass]
    public class ClassTextTest
    {
        [ClassText("UPPER", "1")]
        [ClassText("lower", "2")]
        [ClassText("Mixed", "3")]
        private class KeyTestObject { }
        [TestMethod]
        public void KeyTest()
        {
            var tr = new ClassTextReader(typeof(KeyTestObject));
            Assert.AreEqual(tr["UPPER"], "1");
            Assert.AreEqual(tr["lower"], "2");
            Assert.AreEqual(tr["Mixed"], "3");
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                _ = tr["NotFound"];
            });
        }

        [ClassText("test", "English", "zh-CN:中文")]
        private class I18NClassTextTestObject { }

        [TestMethod]
        public void I18NLanguageSupport()
        {
            var tr = new ClassTextReader(typeof(I18NClassTextTestObject));

            Assert.AreEqual(tr["test"], "English");
            Assert.AreEqual(tr["test", "zh-CN"], "中文");
            Assert.AreEqual(tr["test", "zh-SG"], "English");
        }

        [TestMethod]
        public void StaticClassText()
        {
            var text = ClassTextReaderCache.Acquire(typeof(F));
            Assert.AreEqual("A", text.Get("error_title_fmt", "en-us"));
            Assert.AreEqual("A", text.Get("error_title_fmt", "zh-tw"));
            Assert.AreEqual("a", text.Get("error_title_fmt", "zh-cn"));
        }

        [TestMethod]
        public void MixGetClassText()
        {
            var text = ClassTextReaderCache.Acquire(typeof(F));
            Assert.ThrowsException<InvalidOperationException>(() => text.RxGetClassText("error_title_fmt"));
        }
    }
    [ClassText("error_title_fmt", "A", "zh-cn:a")]
    static class F { }
}
