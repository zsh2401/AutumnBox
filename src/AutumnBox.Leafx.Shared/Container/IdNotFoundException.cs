namespace AutumnBox.Leafx.Container
{
    public class IdNotFoundException : ComponentNotFoundException
    {
        public IdNotFoundException(string id) :
            base($"Id '{id}' has not been found in lake")
        {

        }
    }
}
