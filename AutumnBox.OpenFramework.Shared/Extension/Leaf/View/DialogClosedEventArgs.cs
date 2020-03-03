namespace AutumnBox.OpenFramework.Extension.Leaf
{
    public class DialogClosedEventArgs
    {
        public object Result { get; set; }
        public DialogClosedEventArgs() { }
        public DialogClosedEventArgs(object result)
        {
            Result = result;
        }
    }
}
