using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.I18N;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMGuide : VMSettingsDialog
    {
        public ICommand Next
        {
            get => _next; set
            {
                _next = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _next;

        public ICommand Cancel
        {
            get => _cancel; set
            {
                _cancel = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _cancel;

        public Visibility NextVisibility
        {
            get => _nextVisi; set
            {
                _nextVisi = value;
                RaisePropertyChanged();
            }
        }
        private Visibility _nextVisi = Visibility.Visible;

        public Visibility FinishVisibility
        {
            get => _finishVisi; set
            {
                _finishVisi = value;
                RaisePropertyChanged();
            }
        }
        private Visibility _finishVisi = Visibility.Hidden;

        public FlexiableCommand Prev
        {
            get => _prev; set
            {
                _prev = value;
                RaisePropertyChanged();
            }
        }
        private FlexiableCommand _prev;

        public int Index
        {
            get => _index; set
            {
                _index = value;
                RaisePropertyChanged();
                if (Index == ItemsCount - 1)
                {
                    NextVisibility = Visibility.Hidden;
                    FinishVisibility = Visibility.Visible;
                }
                else
                {
                    NextVisibility = Visibility.Visible;
                    FinishVisibility = Visibility.Hidden;
                }
            }
        }
        private int _index;

        public int ItemsCount
        {
            get => _itemsCount; set
            {
                _itemsCount = value;
                RaisePropertyChanged();
            }
        }
        private int _itemsCount = 4;

        public VMGuide()
        {
            Next = new MVVMCommand(_Next);
            Cancel = DialogHost.CloseDialogCommand;
            Prev = new FlexiableCommand(_Prev)
            {
                CanExecuteProp = false
            };
        }

        private void _Next(object para)
        {
            Prev.CanExecuteProp = true;
            Index++;
        }

        private void _Prev()
        {
            Index--;
            if (Index == 0)
            {
                Prev.CanExecuteProp = false;
            }
        }
    }
}
