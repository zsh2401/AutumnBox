using AutumnBox.Basic.ManagedAdb;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.View.Controls;
using AutumnBox.GUI.View.Slices;
using AutumnBox.GUI.View.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using SliceController = AutumnBox.GUI.Model.SliceController;

namespace AutumnBox.GUI.ViewModel
{
    class VMMainWindowV2 : ViewModelBase
    {
        public IEnumerable<SliceController> Slices
        {
            get
            {
                return slices;
            }
            set
            {
                slices = value.ToList();
                RaisePropertyChanged();
            }
        }
        private List<SliceController> slices;

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
            Slices = new List<SliceController>()
            {
                new SliceController("home","SliceTitleHome",null,home),
                new SliceController("more","SliceTitleMore",null,new More()),
            };
            SelectedSlice = Slices.First();
        }

        public void ShowSlice(object view, string title)
        {
            SelectedSlice = slices[0];
            home.Next(view, title);
        }
    }
}
