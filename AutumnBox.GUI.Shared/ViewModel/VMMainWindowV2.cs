using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.View.Controls;
using AutumnBox.GUI.View.Slices;
using AutumnBox.OpenFramework.Fast;
using AutumnBox.OpenFramework.Management;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SliceController = AutumnBox.GUI.Model.SliceController;

namespace AutumnBox.GUI.ViewModel
{
    class VMMainWindowV2 : ViewModelBase
    {
        public ObservableCollection<SliceController> Slices
        {
            get
            {
                return slices;
            }
            set
            {
                slices = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<SliceController> slices;

        public SliceController SelectedSlice
        {
            get
            {
                return selectedSlice;
            }
            set
            {
                selectedSlice = value;
                RaisePropertyChanged();
            }
        }
        private SliceController selectedSlice;


        private readonly SliceView home = new SliceView(new Home());

        public VMMainWindowV2()
        {
            InitPages();
        }

        public void InitPages()
        {
            Slices = new ObservableCollection<SliceController>()
            {
                new SliceController("home","SliceTitleHome",null,home),
                new SliceController("more","SliceTitleMore",null,new More()),
            };
            InitExtensionPages();
            SelectedSlice = Slices.First();
        }
        private void InitExtensionPages()
        {
            //var wrappers = OpenFx.LibsManager.Wrappers();
            //foreach (var wrapper in wrappers)
            //{
            //    Slices.Add(new SliceController(wrapper.Info.Name,
            //        wrapper.Info.Name,
            //        wrapper.Info.Icon,
            //        new OldExtensionSlice(wrapper)));
            //}
        }

        public void ShowSlice(object view, string title)
        {
            SelectedSlice = slices[0];
            home.Next(view, title);
        }
    }
}
