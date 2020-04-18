using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx.Enhancement.ClassTextKit
{
    public sealed class ClassTextReader
    {
        private ClassTextReader(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            Type = type;
        }

        public Type Type { get; }

        public string this[string key] { get { } }
        public bool TryGet(out string value)
        {
        }

        public static ClassTextReader Load<TClass>()
        {
            return new ClassTextReader(typeof(TClass));
        }
        public static ClassTextReader Load(object instance)
        {
            return new ClassTextReader(instance.GetType());
        }
        public static ClassTextReader Load(Type t)
        {
            return new ClassTextReader(t);
        }
    }
}
