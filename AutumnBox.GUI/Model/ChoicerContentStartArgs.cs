/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/16 18:00:49 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using System;

namespace AutumnBox.GUI.Model
{
    internal enum ChoicerResult
    {
        Cancel,
        Center,
        Right
    }
    internal class ChoicedEventArgs : EventArgs
    {
        public ChoicerResult Result { get; set; }
    }
    class ChoicerContentStartArgs : ModelBase
    {
        public object ContentCancelButton
        {
            get
            {
                return contentCancelButton;
            }
            set
            {
                contentCancelButton = App.Current.Resources[value] ?? value;
                RaisePropertyChanged();
            }
        }
        private object contentCancelButton;

        public object ContentCenterButton
        {
            get
            {
                return contentCenterButton;
            }
            set
            {
                contentCenterButton = App.Current.Resources[value] ?? value;
                RaisePropertyChanged();
            }
        }
        private object contentCenterButton;

        public object ContentRightButton
        {
            get
            {
                return contentRightButton;
            }
            set
            {
                contentRightButton = App.Current.Resources[value] ?? value;
                RaisePropertyChanged();
            }
        }
        private object contentRightButton;

        public object Content
        {
            get
            {
                return content;
            }
            set
            {
                content = App.Current.Resources[value] ?? value;
                RaisePropertyChanged();
            }
        }
        private object content;
        public ChoicerContentStartArgs()
        {
            ContentCancelButton = "btnCancel";
            ContentCenterButton = "btnDeny";
            ContentRightButton = "btnAccept";
        }

        public Action CloseDialog { get; set; } = null;
        public event EventHandler<ChoicedEventArgs> Choiced;
        public void DidChoice(ChoicerResult result)
        {
            Choiced?.Invoke(this, new ChoicedEventArgs() { Result = result });
            CloseDialog();
        }
    }
}
