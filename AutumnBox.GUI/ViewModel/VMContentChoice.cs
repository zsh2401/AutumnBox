/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/16 17:20:27 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using System;
using static AutumnBox.GUI.View.DialogContent.ContentChoice;

namespace AutumnBox.GUI.ViewModel
{
    class VMContentChoice : ViewModelBase
    {
        public ChoicerContentStartArgs Model { get; private set; }

        public VMContentChoice(ChoicerContentStartArgs args)
        {
            Model = args;
            DoChoice = new FlexiableCommand((para) =>
            {
                switch (para)
                {
                    case "R":
                        args.DidChoice(ChoicerResult.Right);
                        break;
                    case "C":
                        args.DidChoice(ChoicerResult.Center);
                        break;
                    default:
                        args.DidChoice(ChoicerResult.Cancel);
                        break;
                }
            });
        }

        public Action CloseDialog { get; set; }

        public FlexiableCommand DoChoice
        {
            get
            {
                return doChoice;
            }
            set
            {
                doChoice = value;
                RaisePropertyChanged();
            }
        }
        private FlexiableCommand doChoice;

    }
}
