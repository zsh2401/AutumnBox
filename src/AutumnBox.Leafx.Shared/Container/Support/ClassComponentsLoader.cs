using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable enable
namespace AutumnBox.Leafx.Container.Support
{
    /// <summary>
    /// 表示程序集组件加载器，用于进行广泛的组件加载
    /// <code>
    /// var loader = new ClassComponentsLoader("Example.NameSpace",lake,AppDomain.CurrentDomain.GetAssemblies());
    /// loader.Do();
    /// </code>
    /// </summary>
    public sealed class ClassComponentsLoader
    {
        private readonly string prefix;
        private readonly IRegisterableLake target;
        private readonly Assembly[] assemblies;

        /// <summary>
        /// 初始化ClassComponentsLoader
        /// </summary>
        /// <param name="prefix">命名空间前缀,如AutumnBox.GUI.Services</param>
        /// <param name="target">被扫描组件的目标加载位置</param>
        /// <param name="assemblies">指定扫描的程序集,如果不指定,则扫描全面当前APP加载的程序集</param>
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

            this.prefix = prefix;
            this.target = target;
            this.assemblies = assemblies?.Any() == true ? assemblies : AppDomain.CurrentDomain.GetAssemblies();
        }

        /// <summary>
        /// 执行
        /// </summary>
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
