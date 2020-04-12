using System;
using System.Threading.Tasks;

namespace AutumnBox.Leafx.Routines
{
    public interface IRoutinesManager
    {
        Task<object> Run<TRoutine>();
        Task<object> Run(Type routine);
    }
}
