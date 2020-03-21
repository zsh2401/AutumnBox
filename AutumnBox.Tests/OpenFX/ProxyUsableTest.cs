using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.OpenFX
{
    [TestClass]
    class ProxyUsableTest
    {
        private interface ITestable
        {
            void Test();
        }
        private class TestImpl : ITestable
        {
            public void Test()
            {
                Debug.WriteLine("Test");
            }
        }
        [TestMethod]
        public void Test()
        {
            var result = (ITestable)CreateProxy(typeof(ITestable), typeof(TestImpl));
            result.Test();
        }
        private object CreateProxy(Type _interface, Type impl)
        {
            AppDomain ad = AppDomain.CurrentDomain;
            AssemblyName an = new AssemblyName("AtmbProxyObjects");
            AssemblyBuilder asmBuilder = ad.DefineDynamicAssembly(
                     an,
                     AssemblyBuilderAccess.RunAndSave);
            var fModule = asmBuilder.DefineDynamicModule("fuckingmodule", "tmp.dll");
            var typeBuilder = fModule.DefineType(impl.FullName + "_s_proxy", TypeAttributes.Public);

            foreach (var pMethod in impl.GetMethods(BindingFlags.Public))
            {
                var paraTypes = pMethod.GetParameters()
                    .Select((pInfo) => pInfo.ParameterType)
                    .ToArray();
                var methodBuilder = typeBuilder.DefineMethod(pMethod.Name,
                    MethodAttributes.Public,
                    pMethod.ReturnType,
                    paraTypes);
            }
            typeBuilder.AddInterfaceImplementation(_interface);
        }
    }
}
