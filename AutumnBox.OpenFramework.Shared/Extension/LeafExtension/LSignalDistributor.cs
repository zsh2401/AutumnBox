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
            public void Do(string message, object value)
            {
                bool executed = false;
                try
                {
                    method.Invoke(owner, new object[] { message, value });
                    executed = true;
                }
                catch (TargetParameterCountException) { }
                if (executed) return;
                try
                {
                    method.Invoke(owner, new object[] { message });
                    executed = true;
                }
                catch (TargetParameterCountException) { }
                if (executed) return;
                try
                {
                    method.Invoke(owner, new object[] { value });
                    executed = true;
                }
                catch (TargetParameterCountException) { }
                if (executed) return;
                try
                {
                    method.Invoke(owner, new object[] { });
                    executed = true;
                }
                catch (TargetParameterCountException) { }
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
        }
        public void Receive(string message, object value)
        {
            var canReceive = from method in wrappers
                             where method.ReceiveSignal == message || method.ReceiveSignal == null
                             select method;
            foreach (var method in canReceive)
            {
                method.Do(message, value);
            }
        }
    }
}
