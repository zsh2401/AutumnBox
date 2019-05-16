using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.View.Controls;
using AutumnBox.GUI.View.Slices;
using AutumnBox.GUI.View.Windows;
using System.Collections.Generic;
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
                SelectionChanged();
                RaisePropertyChanged();
            }
        }
        private SliceController selectedSlice;


        public ICommand Exit { get; set; }
        public ICommand OpenLoggingWindow { get; set; }
        public ICommand UpdateLogs { get; set; }
        public ICommand Settings { get; set; }

        private readonly SliceView home = new SliceView(new Home());

        public VMMainWindowV2()
        {
            InitCommand();
            InitPages();
        }

        private void SelectionChanged()
        {
            //if(SelectedPage)
        }

        public void InitPages()
        {
            Slices = new List<SliceController>()
            {
                new SliceController("home","Welcome to AutumnBox",null,home),
                new SliceController("more","More",null,new More()),
            };
            SelectedSlice = Slices.First();
        }

        public void InitCommand()
        {
            Exit = new MVVMCommand(p => { App.Current.Shutdown(0); });
            OpenLoggingWindow = new MVVMCommand(p => { new LogWindow().Show(); });
            UpdateLogs = new MVVMCommand(p => ShowSlice("testXXX"));
            Settings = new MVVMCommand(p => ShowSlice(new Settings()));
        }

        public void ShowSlice(object view)
        {
            SelectedSlice = slices[0];
            home.Next(view);
        }
    }
}
