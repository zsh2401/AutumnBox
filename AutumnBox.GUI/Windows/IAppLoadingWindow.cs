namespace AutumnBox.GUI.Windows
{
    internal interface IAppLoadingWindow
    {
        void SetProgress(double value);
        void SetTip(string value);
        void Finish();
    }
}
