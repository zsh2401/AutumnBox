using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx.Container
{
    public enum RecordSettings : int
    {
        Singleton = 1 << 0,
        Disposable = 1 << 1,
    }
    public interface ILakeRecord
    {
        string Id { get; }
        RecordSettings Settings { get; }
        Func<object> Factory { get; }
    }
}
