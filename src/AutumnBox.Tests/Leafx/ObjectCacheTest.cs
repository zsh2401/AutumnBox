/*

* ==============================================================================
*
* Filename: CacheTest
* Description: 
*
* Version: 1.0
* Created: 2020/5/5 14:42:30
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.ObjectManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.Leafx
{
    [TestClass]
    public class ObjectCacheTest
    {
        [TestMethod]
        public void ReferenceEqualsTest()
        {
            var acquire0_0 = ObjectCache<int, object>.Acquire(0, () => new object());
            var acquire0_1 = ObjectCache<int, object>.Acquire(0, () => new object());
            Assert.IsTrue(ReferenceEquals(acquire0_0, acquire0_1));

            var acquire1 = ObjectCache<int, object>.Acquire(1, () => new object());
            var acquire2 = ObjectCache<int, object>.Acquire(2, () => new object());
            Assert.IsFalse(ReferenceEquals(acquire1, acquire2));

            acquire0_0 = ObjectCache<int, object>.Acquire(0, () => new object());
            acquire0_1 = ObjectCache<int, object>.Acquire(0, () => new object());
            Assert.IsTrue(ReferenceEquals(acquire0_0, acquire0_1));
        }
        [TestMethod]
        public void CountTest()
        {
            _ = ObjectCache<string, object>.Acquire("1", () => new object());
            _ = ObjectCache<string, object>.Acquire("2", () => new object());
            Assert.IsTrue(ObjectCache<string, object>.CachedCount == 2);

            _ = ObjectCache<int, int>.Acquire(0, () => 0);
            _ = ObjectCache<int, int>.Acquire(1, () => 1);
            Assert.IsTrue(ObjectCache<int, int>.CachedCount == 2);
        }

        [TestMethod]
        public void ClearCacheTest()
        {
            _ = ObjectCache<string, object>.Acquire("1", () => new object());
            _ = ObjectCache<string, object>.Acquire("2", () => new object());
            ObjectCache<string, object>.ClearCache();
            Assert.IsTrue(ObjectCache<string, object>.CachedCount == 0);
        }

        [TestMethod]
        public void HighConcurrencyAcquire()
        {
            const int totalTimes = 100_000;
            const int taskCount = 3;
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < taskCount; i++)
            {
                tasks.Add(Task.Run(task));
            }
            Task.WaitAll(tasks.ToArray());
            Assert.IsTrue(ObjectCache<string, float>.CachedCount == taskCount * totalTimes);
            static void task()
            {
                for (int i = 0; i < totalTimes; i++)
                {
                    ObjectCache<string, float>.Acquire(Guid.NewGuid().ToString(), () => 1.0F);
                }
            }
        }
    }
}
