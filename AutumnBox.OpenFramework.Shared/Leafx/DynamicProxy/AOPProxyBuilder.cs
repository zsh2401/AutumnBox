/*

* ==============================================================================
*
* Filename: AOPProxyBuilder
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 13:31:52
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;

namespace AutumnBox.OpenFramework.Leafx.DynamicProxy
{
    public class AOPProxyBuilder : IAOPProxyBuilder
    {
        private class MObject : MarshalByRefObject
        {
            public override ObjRef CreateObjRef(Type requestedType)
            {
                return base.CreateObjRef(requestedType);
            }
        }
        private class AOPProxy : RealProxy
        {
            public AOPProxy(ILake lake, Type implType, Type interfaceType) : base(interfaceType) { }
            public override IMessage Invoke(IMessage msg)
            {
                foreach (DictionaryEntry entry in msg.Properties)
                {
                    Debug.WriteLine($"{entry.Key}-{entry.Value}");
                }
            }
        }
        public object Build(Type t)
        {
            return new AOPProxy(t).GetTransparentProxy();
        }
    }
}
