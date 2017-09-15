using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Executer
{
    public struct OutErrorData
    {
        public List<string> LineOut { get; internal set; }
        public List<string> LineError { get; internal set; }
        public StringBuilder Out { get; internal set; }
        public StringBuilder Error { get; internal set; }
        internal void Init()
        {
            LineError = new List<string>();
            LineOut = new List<string>();
            Out = new StringBuilder();
            Error = new StringBuilder();
        }
        internal void OutAdd(string Out)
        {
            LineOut.Add(Out);
            this.Out.AppendLine(Out);
        }
        internal void ErrorAdd(string Error)
        {
            LineError.Add(Error);
            this.Error.AppendLine(Error);
        }
        internal void Clear()
        {
            LineOut.Clear();
            LineError.Clear();
            Out.Clear();
            Error.Clear();
        }
        internal static OutErrorData Get()
        {
            OutErrorData o = new OutErrorData();
            o.Init();
            return o;
        }
        internal static void Get(out OutErrorData d)
        {
            d = Get();
        }
    }
}
