#nullable enable
namespace AutumnBox.OpenFramework.Open.LKit
{
    public class DialogClosedEventArgs
    {
        public object? Result { get; set; } = null;
        public DialogClosedEventArgs() { }
        public DialogClosedEventArgs(object result)
        {
            Result = result;
        }
    }
}
