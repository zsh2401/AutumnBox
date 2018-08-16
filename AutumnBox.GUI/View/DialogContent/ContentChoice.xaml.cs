using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.ViewModel;
using System;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.DialogContent
{
    /// <summary>
    /// ContentChoice.xaml 的交互逻辑
    /// </summary>
    public partial class ContentChoice : UserControl
    {


        internal VMContentChoice ViewModel
        {
            get
            {
                return (DataContext as VMContentChoice);
            }
            private set
            {
                DataContext = value;
            }
        }
        internal ContentChoice(ChoicerContentStartArgs args)
        {
            InitializeComponent();
            ViewModel = new VMContentChoice(args);
        }
    }
}
