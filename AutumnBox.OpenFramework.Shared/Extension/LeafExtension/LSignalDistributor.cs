using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    internal sealed class LSignalDistributor
    {
        private readonly LeafExtensionBase ext;
        private class ReceiverWrapper
        {
            private readonly object owner;
            private readonly MethodInfo method;
            private readonly LSignalReceiveAttribute attribute;
            private readonly bool isReceiveValue;
            public string ReceiveSignal { get; }
            public ReceiverWrapper(object owner, MethodInfo method, LSignalReceiveAttribute attribute)
            {
                this.owner = owner;
                this.method = method;
                this.attribute = attribute;
                this.ReceiveSignal = attribute.Pattern;
                var paras = method.GetParameters();
                isReceiveValue = (paras.Count() == 1);
            }
            public void Do(object value)
            {
                if (isReceiveValue)
                {
                    method.Invoke(owner, new object[] { value });
                }
                else
                {
                    method.Invoke(owner, new object[] { });
                }
            }
        }
        private readonly List<ReceiverWrapper> wrappers = new List<ReceiverWrapper>();
        public LSignalDistributor(LeafExtensionBase ext)
        {
            this.ext = ext ?? throw new ArgumentNullException(nameof(ext));
        }
        public void ScanReceiver()
        {
            var methods = ext.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Default);
            foreach (var method in methods)
            {
                var attr = method.GetCustomAttribute(typeof(LSignalReceiveAttribute));
                if (attr != null)
                {
                    wrappers.Add(new ReceiverWrapper(ext, method, (LSignalReceiveAttribute)attr));
                }
            }
            Trace.WriteLine($"scanned receiver {wrappers.Count()}");
        }
        public void Receive(string message, object value)
        {
            Trace.WriteLine($"received {message} {value}");
            var canReceive = from method in wrappers
                             where method.ReceiveSignal == message || method.ReceiveSignal == null
                             select method;
            foreach (var method in canReceive)
            {
                method.Do(value);
            }
        }
    }
}
