using AutumnBox.GUI.MVVM;

namespace AutumnBox.GUI.ViewModel
{
    class VMGuide : VMSettingsDialog
    {
        public FlexiableCommand Next
        {
            get => _next; set
            {
                _next = value;
                RaisePropertyChanged();
            }
        }
        private FlexiableCommand _next;

        public FlexiableCommand Cancel
        {
            get => _cancel; set
            {
                _cancel = value;
                RaisePropertyChanged();
            }
        }
        private FlexiableCommand _cancel;

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
                    Next.CanExecuteProp = false;
                    Prev.CanExecuteProp = true;
                }
                else if (Index == 0)
                {
                    Next.CanExecuteProp = true;
                    Prev.CanExecuteProp = false;
                }
                else
                {
                    Next.CanExecuteProp = true;
                    Prev.CanExecuteProp = true;
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
            Next = new FlexiableCommand(_Next);
            Prev = new FlexiableCommand(_Prev)
            {
                CanExecuteProp = false
            };
        }

        private void _Next(object para)
        {
            Index++;
        }

        private void _Prev()
        {
            Index--;
        }
    }
}
