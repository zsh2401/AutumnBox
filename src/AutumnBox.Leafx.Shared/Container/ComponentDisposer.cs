using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx.Container
{
    class ComponentDisposer
    {
        private readonly ILake[] source;

        public ComponentDisposer(params ILake[] source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            this.source = source;
        }
        public void Free()
        {
            foreach (var lake in source)
            {

            }
        }
        public static void Free(params ILake[] source)
        {
            new ComponentDisposer(source).Free();
        }
    }
}
