using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.ViewModel
{
    class VMMainWindowV2 : ViewModelBase
    {
        public IEnumerable<IAtmbViewItem> ViewItems
        {
            get
            {
                return viewItems;
            }
            set
            {
                viewItems = value;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<IAtmbViewItem> viewItems;

        public ICommand Exit { get; set; }
        public ICommand OpenLoggingWindow { get; set; }

        public VMMainWindowV2()
        {
            InitCommand();
        }
        public void InitCommand()
        {
            Exit = new MVVMCommand(p => { App.Current.Shutdown(0); });
            OpenLoggingWindow = new MVVMCommand(p => { new LogWindow().Show(); });
            viewItems = new List<IAtmbViewItem>() {
                new DefaultView(null,"Welcome to AutumnBox",new TextBlock() { Text="hehe"}),
                new DefaultView(null,"Poweron usable",new TextBlock() { Text="heheB"}),
                new DefaultView(null,"Recovery usable",new TextBlock() { Text="heheX"}),
            };
        }
    }
}
