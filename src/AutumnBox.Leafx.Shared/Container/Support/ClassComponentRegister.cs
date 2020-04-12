using System;
using System.Reflection;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.ObjectManagement;

namespace AutumnBox.Leafx.Container.Support
{
    public static class ClassComponentRegister
    {
        public static void Register(this IRegisterableLake lake, Type t)
        {
            var attr = t.GetCustomAttribute<ComponentAttribute>();
            if (attr.Id != null)
            {
                if (attr.SingletonMode)
                {
                    lake.RegisterSingleton(attr.Id, () => new ObjectBuilder(t, lake).Build());
                }
                else
                {
                    lake.Register(attr.Id, () => new ObjectBuilder(t, lake).Build());
                }
            }
            else
            {
                if (attr.SingletonMode)
                {
                    lake.RegisterSingleton(attr.Type, () => new ObjectBuilder(t, lake).Build());
                }
                else
                {
                    lake.Register(attr.Type, () => new ObjectBuilder(t, lake).Build());
                }
            }
        }
    }
}
