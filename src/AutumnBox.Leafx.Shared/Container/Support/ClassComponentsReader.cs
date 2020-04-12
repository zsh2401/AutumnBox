using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutumnBox.Leafx.Container.Support
{
    public sealed class ClassComponentsReader
    {
        private readonly string prefix;
        private readonly IRegisterableLake target;

        public ClassComponentsReader(string prefix, IRegisterableLake target)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                throw new ArgumentException("message", nameof(prefix));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }


            this.prefix = prefix;
            this.target = target;
        }

        public void Do()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> componentsResult = new List<Type>();
            foreach (var assembly in assemblies)
            {
                var components = from type in assembly.GetTypes()
                                 where type.FullName.StartsWith(prefix)
                                 where type.GetCustomAttribute<ComponentAttribute>() != null
                                 where type.GetCustomAttribute<ComponentAttribute>().Type != null || type.GetCustomAttribute<ComponentAttribute>().Id != null
                                 select type;
                componentsResult.AddRange(components);
            }

            componentsResult.ForEach(t =>
            {
                try
                {
                    ClassComponentRegister.Register(target, t);
                }
                catch (Exception e)
                {
                    SLogger<ClassComponentsReader>.Exception(e);
                }
            });
        }
    }
}
