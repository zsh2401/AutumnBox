/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 19:18:37 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Helper;
using AutumnBox.OpenFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Mods
{
    public static class ModManager
    {
        public static AutumnBoxMod[] Mods
        {
            get
            {
                return mods.ToArray();
            }
        }
        public static void InitAll()
        {
            mods.ForEach((mod) =>
            {
                mod.Init(new InitArgs());
            });
        }
        public static void DestoryAll()
        {
            mods.ForEach((mod) =>
            {
                mod.Destory(new DestoryArgs());
            });
        }

        private static List<AutumnBoxMod> mods;
        static ModManager()
        {
            AutumnGuiApi.Init(ShowChoiceWindow, BoxHelper.ShowMessageDialog, BoxHelper.ShowLoadingDialog);
            mods = new List<AutumnBoxMod>();
            Scan();
        }
        private static void Scan()
        {
            mods.Clear();
            var files = GetModDllFilePaths();
            var dlls = LoadDlls(files);
            mods.AddRange(LoadMods(dlls));
            mods.RemoveAll((mod)=> {
                return mod.TARGET_SDK != BuildInfo.SDK_VERSION;
            });
        }
        private static string[] GetModDllFilePaths()
        {
            var modPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\AutumnBox_Mods";
            if (Directory.Exists(modPath) == false)
            {
                Directory.CreateDirectory(modPath);
            }
            return Directory.GetFiles(modPath, "*.dll");
        }
        private static Assembly[] LoadDlls(string[] dllFilePaths)
        {
            List<Assembly> dlls = new List<Assembly>();
            foreach (var dllPath in dllFilePaths)
            {
                try { dlls.Add(Assembly.LoadFile(dllPath)); } catch { }
            }
            return dlls.ToArray();
        }
        private static List<AutumnBoxMod> LoadMods(Assembly[] dlls)
        {
            List<AutumnBoxMod> mods = new List<AutumnBoxMod>();
            foreach (var dll in dlls)
            {
                try
                {
                    mods.AddRange(
                        from type in dll.GetExportedTypes()
                        where type.BaseType == typeof(AutumnBoxMod)
                        select (AutumnBoxMod)Activator.CreateInstance(type));
                }
                catch { }
            }
            return mods;
        }
        private static bool? ShowChoiceWindow(string t, string m, string l, string r)
        {
            var choiceResult = BoxHelper.ShowChoiceDialog(t, m, l, r);
            switch (choiceResult)
            {
                case Windows.ChoiceResult.BtnRight:
                    return true;
                case Windows.ChoiceResult.BtnLeft:
                    return true;
                default:
                    return null;
            }
        }
    }
}
