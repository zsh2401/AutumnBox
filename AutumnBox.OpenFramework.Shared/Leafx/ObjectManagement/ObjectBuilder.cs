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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutumnBox.OpenFramework.Leafx.ObjectManagement
{
    /// <summary>
    /// 对象构造器
    /// </summary>
    public sealed class ObjectBuilder
    {
        private readonly Type type;

        public List<ILake> Sources { get; set; }

        public ObjectBuilder(Type type, params ILake[] sources)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            this.type = type;
            Sources = sources.ToList();
        }

        public object Build(Dictionary<string, object> extraArgs = null)
        {
            var constructor = type.GetConstructors()[0];
            var args = ArgsBuilder.BuildArgs(
                Sources,
                extraArgs ?? new Dictionary<string, object>(), constructor.GetParameters());
            var instance =  constructor.Invoke(args);
            new PropertyInjector(instance, Sources.ToArray()).Inject();
            return instance;
        }
    }
}
