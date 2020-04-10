/*

* ==============================================================================
*
* Filename: DependingObject
* Description: 
*
* Version: 1.0
* Created: 2020/4/10 22:25:59
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using AutumnBox.Leafx.Container;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutumnBox.Leafx.ObjectManagement
{
    public abstract class DependingObject
    {
        protected List<ILake> Sources { get; set; }
        protected virtual bool InjectOnCreate => true;
        public DependingObject(params ILake[] sources)
        {
            Sources = sources.ToList();
            if (InjectOnCreate)
            {
                InvokeMethod(nameof(InjectProperty));
            }
        }
        protected void InjectProperty()
        {
            new PropertyInjector(this, Sources.ToArray()).Inject();
        }
        protected object InvokeMethod(string methodName, Dictionary<string, object> args = null)
        {
            var methodProxy = new MethodProxy(this, methodName, Sources.ToArray());
            return methodProxy.GetInvoker(args).Invoke();
        }
        protected object GetValue(string id)
        {
            return Sources.Get(id);
        }
        protected object GetValue(Type t)
        {
            return Sources.Get(t);
        }
        protected T GetValue<T>()
        {
            return (T)GetValue(typeof(T));
        }
    }
}
