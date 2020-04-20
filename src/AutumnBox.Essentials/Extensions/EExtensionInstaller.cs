using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    class EExtensionInstaller : LeafExtensionBase
    {
        [AutoInject]
        private readonly IAppManager appManager;
        [LMain]
        public void EntryPoint()
        {
            
        }
        //private bool TrySelectDllFile(out string[] files)
        //{
        //    string[] result = null;
        //    bool? selectResult = false;
        //    appManager.RunOnUIThread(() =>
        //    {
        //        OpenFileDialog fileDialog = new OpenFileDialog();
        //        fileDialog.Reset();
        //        fileDialog.Title = Text["title"];
        //        fileDialog.Filter = Text["filter"];
        //        fileDialog.Multiselect = true;
        //        selectResult = fileDialog.ShowDialog();
        //        result = fileDialog.FileNames;
        //    });
        //    files = result;
        //    return selectResult == true;
        //}
    }
}
