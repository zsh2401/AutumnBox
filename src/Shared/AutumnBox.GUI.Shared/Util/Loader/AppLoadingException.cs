using System;

namespace AutumnBox.GUI.Util.Loader
{
    class AppLoadingException : Exception
    {
        public AppLoadingException(string stepName, Exception innerException)

            : base($"can not execute load step: {stepName}", innerException) {
            StepName = stepName;
        }

        public string StepName { get; }
    }
}
