using AutumnBox.GUI.MVVM;
using System;
using System.Windows.Media;

namespace AutumnBox.GUI.ViewModel
{
    class VMInputWindow : ViewModelBase
    {
        public string Hint
        {
            get { return _hint; }
            set
            {
                _hint = App.Current.Resources[value] as string ?? value;
                RaisePropertyChanged();
            }
        }
        private string _hint;

        public string Input
        {
            get { return _input; }
            set
            {
                _input = value;
                RaisePropertyChanged();
            }
        }
        private string _input;

        public Brush TextBoxBorderBrush
        {
            get => _textBoxBorderBrush; set
            {
                _textBoxBorderBrush = value;
                RaisePropertyChanged();
            }
        }
        private Brush _textBoxBorderBrush;

        public MVVMCommand Finish
        {
            get => _finish; set
            {
                _finish = value;
                RaisePropertyChanged();
            }
        }
        private MVVMCommand _finish;

        public MVVMCommand Cancel
        {
            get => _cancel; set
            {
                _cancel = value;
                RaisePropertyChanged();
            }
        }
        private MVVMCommand _cancel;

        public Action<string> CloseAction { get; set; }
        public Func<string, bool> InputCheck { get; set; }

        private static readonly Brush DftBrush = new SolidColorBrush(Colors.Gray);
        private static readonly Brush ErrorBrush = new SolidColorBrush(Colors.Red);
        
        public VMInputWindow()
        {
            TextBoxBorderBrush = DftBrush;
            Finish = new MVVMCommand((_p) =>
            {
                if (InputCheck(Input))
                {
                    CloseAction(Input);
                }
                else
                {
                    TextBoxBorderBrush = ErrorBrush;
                }
            });
            Cancel = new MVVMCommand((_p) =>
            {
                CloseAction(null);
            });
        }
    }
}
