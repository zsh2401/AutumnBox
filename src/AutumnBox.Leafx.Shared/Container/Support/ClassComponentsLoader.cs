using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutumnBox.Leafx.Container.Support
{
    public sealed class ClassComponentsLoader
    {
        private readonly string prefix;
        private readonly IRegisterableLake target;
        private readonly Assembly[] assemblies;

        public ClassComponentsLoader(string prefix, IRegisterableLake target, params Assembly[] assemblies)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                throw new ArgumentException("message", nameof(prefix));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (assemblies is null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            this.prefix = prefix;
            this.target = target;
            this.assemblies = assemblies.Any() ? assemblies : AppDomain.CurrentDomain.GetAssemblies();
        }

        public void Do()
        {
            List<Type> componentsResult = new List<Type>();
            foreach (var assembly in assemblies)
            {
                var components = from type in assembly.GetTypes()
                                 where type.FullName.StartsWith(prefix)
                                 where type.GetCustomAttribute<ComponentAttribute>() != null
                                 select type;
                componentsResult.AddRange(components);
            }

            SLogger<ClassComponentsLoader>.Info($"Found {componentsResult.Count()} class component");
            componentsResult.ForEach(t =>
            {
                try
                {
                    ClassComponentRegister.Register(target, t);
                }
                catch (Exception e)
                {
                    SLogger<ClassComponentsLoader>.Exception(e);
                }
            });
        }
    }
}
