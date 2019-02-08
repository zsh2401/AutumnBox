using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using Microsoft.Win32;
using System;
using System.IO;

namespace AutumnBox.CoreModules.Extensions.NoMatter
{
    [ExtName("Install Extension","zh-cn:安装拓展模块")]
    [ExtAuth("zsh2401")]
    [ExtPriority(ExtPriority.HIGH)]
    [ExtIcon("Icons.add.png")]
    [ExtMinApi(8)]
    [ExtTargetApi(8)]
    [ContextPermission(CtxPer.High)]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    class EInstallExtension : LeafExtensionBase
    {
        [LMain]
        public void Main(IUx ux, ILeafUI ui, IAppManager app)
        {
            using (ui)
            {
                if (TrySelectDllFile(ux, out string[] files))
                {
                    ui.Show();
                    ui.Icon = this.GetIconBytes();
                    string extensionPath = BuildInfo.DEFAULT_EXTENSION_PATH;
                    int copied = 0;
                    foreach (var filepath in files)
                    {
                        FileInfo file = new FileInfo(filepath);
                        string target = Path.Combine(extensionPath, file.Name);
                        file.CopyTo(target,true);
                        copied++;
                        ui.Progress = (int)(Convert.ToDouble(copied) /  files.Length * 100);
                    }
                    app.RestartApp();
                }
                ui.Shutdown();
            }
        }

        private bool TrySelectDllFile(IUx ux, out string[] files)
        {
            string[] result = null;
            bool? selectResult = false;
            ux.RunOnUIThread(() =>
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Reset();
                fileDialog.Title = CoreLib.Current.Languages.Get("EInstallExtensionFileDialogTitle");
                fileDialog.Filter = CoreLib.Current.Languages.Get("EInstallExtensionFileDialogFilter");
                fileDialog.Multiselect = true;
                selectResult = fileDialog.ShowDialog();
                result = fileDialog.FileNames;
            });
            files = result;
            return selectResult == true;
        }
    }
}
