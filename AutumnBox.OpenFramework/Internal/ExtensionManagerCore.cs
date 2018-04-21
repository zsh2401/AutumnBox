/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/14 23:16:05 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Internal
{
    internal sealed class ExtensionManagerCore : Context
    {
        public List<IAutumnBoxExtension> Extensions { get; private set; }

        public ExtensionManagerCore()
        {
            Extensions = new List<IAutumnBoxExtension>();
            if (Directory.Exists(ExtensionManager.ExtensionsPath) == false)
            {
                Directory.CreateDirectory(ExtensionManager.ExtensionsPath);
            }
        }

        public void Load()
        {
            List<Type> extensionTypes = new List<Type>();
            List<Assembly> dllAssemblies = new List<Assembly>();
            string[] dllFiles = Directory.GetFiles(ExtensionManager.ExtensionsPath, "*.dll");
            OpenApi.Log.Info(this, $"已获取{dllFiles.Length}个文件");
            foreach (string file in dllFiles)
            {
                try
                {
                    dllAssemblies.Add(Assembly.LoadFrom(file));
                }
                catch (Exception ex)
                {
                    OpenApi.Log.Info(this, "加载一个程序集时发生异常\n" + ex);
                }
            }
            foreach (Assembly dll in dllAssemblies)
            {
                try
                {
                    var types = dll.GetExportedTypes()
                    .Where((extType) => { return typeof(IAutumnBoxExtension).IsAssignableFrom(extType); });
                    if (types.Count() == 0)
                    {
                        throw new Exception("No AutumnBox Extension");
                    }
                    extensionTypes.AddRange(types);
                }
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, $"获取程序集{dll.FullName}的拓展类型时发生错误\n" + ex);
                }
            }
            var initArgs = new InitArgs();
            foreach (Type extensionType in extensionTypes)
            {
                try
                {
                    var extInstance = (IAutumnBoxExtension)Activator.CreateInstance(extensionType);
                    if (extInstance.Init(initArgs) != true) continue;
                    Extensions.Add(extInstance);
                }
                catch (Exception ex)
                {
                    OpenApi.Log.Warn(this, $"加载{extensionType.Name}时发生错误\n" + ex);
                }
            }
        }

        //public static IEnumerable<IAutumnBoxExtension> GetScripts()
        //{
        //    string[] scriptFiles = Directory.GetFiles(ExtensionManager.ExtensionsPath, "*.cs");
        //}

        internal override ContextPermissionLevel GetPermissionLevel()
        {
            return ContextPermissionLevel.High;
        }

        ~ExtensionManagerCore()
        {
            DestoryArgs args = new DestoryArgs();
            Extensions.ForEach((ext) =>
            {
                try { ext.Destory(args); }
                catch { }
            });
        }
    }
}
