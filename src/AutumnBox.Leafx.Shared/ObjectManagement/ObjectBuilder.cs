/*

* ==============================================================================
*
* Filename: ObjectBuilder
* Description: 
*
* Version: 1.0
* Created: 2020/4/5 21:57:45
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
    /// <summary>
    /// 对象构造器
    /// </summary>
    public sealed class ObjectBuilder
    {
        private readonly Type type;
        private readonly ILake source;

        public ObjectBuilder(Type type, ILake source)
        {
            this.type = type ?? throw new ArgumentNullException(nameof(type));
            this.source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public object Build(Dictionary<string, object> extraArgs = null)
        {
            var constructor = type.GetConstructors(ObjectManagementConstants.BINDING_FLAGS)[0];
            var args = ParameterArrayBuilder.BuildArgs(
                source,
                extraArgs ?? new Dictionary<string, object>(), constructor.GetParameters());
            var instance =  constructor.Invoke(args);
            new DependenciesInjector(instance, source).Inject();
            return instance;
        }
    }
}
