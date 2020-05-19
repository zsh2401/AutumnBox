using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Loader
{
    class StepFinishedEventArgs : EventArgs
    {
        public StepFinishedEventArgs(uint finishedStep, uint totalStepCount) {
            FinishedStep = finishedStep;
            TotalStepCount = totalStepCount;
        }

        public uint FinishedStep { get; }
        public uint TotalStepCount { get; }
    }
}
