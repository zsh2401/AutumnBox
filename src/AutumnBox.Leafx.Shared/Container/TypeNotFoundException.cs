using System;

namespace AutumnBox.Leafx.Container
{
    public class TypeNotFoundException : ComponentNotFoundException
    {
        public TypeNotFoundException(Type t) :
            base($"Type {t.FullName} not found in lake")
        {
        }
    }
}
