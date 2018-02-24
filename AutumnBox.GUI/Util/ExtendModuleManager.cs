/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:36:04 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.FlowFramework;
using AutumnBox.GUI.Helper;
using AutumnBox.OpenFramework;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.Util
{
    internal static class ExtendModuleManager
    {
        private static List<AutumnBoxExtendModule> modules;
        public static AutumnBoxExtendModule[] GetModules()
        {
            return modules.ToArray();
        }

        public static void Load()
        {
            OpenApi.Gui = new GuiApi();
            OpenApi.Log = new LogApi();
            var dlls = GetDlls();
            LoadAllExtModule(dlls);
        }
        private static List<Assembly> GetDlls()
        {
            var modPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\AutumnBox_Mods_X";
            if (Directory.Exists(modPath) == false)
            {
                Directory.CreateDirectory(modPath);
            }
            var dllFilePaths = Directory.GetFiles(modPath);
            var dlls = new List<Assembly>();
            foreach (var dllFilePath in dllFilePaths)
            {
                try
                {
                    dlls.Add(Assembly.LoadFile(dllFilePath));
                }
                catch { }
            }
            return dlls;
        }
        private static void LoadAllExtModule(List<Assembly> dlls)
        {
            modules = new List<AutumnBoxExtendModule>();
            dlls.ForEach((dll) =>
            {
                var types = from type in dll.GetExportedTypes()
                            where type.BaseType == typeof(AutumnBoxExtendModule)
                            select type;
                foreach (Type type in types)
                {
                    try
                    {
                        var module = (AutumnBoxExtendModule)Activator.CreateInstance(type);
                        modules.Add(module);
                    }
                    catch { }
                }
            });
        }


        private class GuiApi : IGuiApi
        {
            public bool? ShowChoiceBox(string title, string msg, string btnLeft = null, string btnRight = null)
            {
                return BoxHelper.ShowChoiceDialog(title, msg, btnLeft, btnRight).ToBool();
            }
            public void ShowLoadingWindow(ICompletable completable)
            {
                BoxHelper.ShowLoadingDialog(completable);
            }
            public void ShowMessageBox(string title, string msg)
            {
                BoxHelper.ShowMessageDialog(title, msg);
            }
            public void ShowWindow(Window window)
            {
                window.Show();
            }
        }
        private class LogApi : ILogApi
        {
            public void Log(string tag, string msg)
            {
                Logger.T(msg);
            }
        }
    }
}
