using System;

namespace AutumnBox.GUI.Util.Loader
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class StepAttribute : Attribute
    {
        public StepAttribute(uint step)
        {
            Step = step;
        }

        public uint Step { get; }
    }
}
