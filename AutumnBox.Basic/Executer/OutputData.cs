using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Executer
{
    public class OutputData : BaseObject
    {
        public List<string> LineAll { get; private set; }
        public List<string> LineOut { get; private set; }
        public List<string> LineError { get; private set; }
        public StringBuilder All { get; private set; }
        public StringBuilder Out { get; private set; }
        public StringBuilder Error { get; private set; }
        internal void OutAdd(string Out)
        {
            All.AppendLine(Out);
            LineAll.Add(Out);
            LineOut.Add(Out);
            this.Out.AppendLine(Out);
        }
        internal void ErrorAdd(string Error)
        {
            All.AppendLine(Error);
            LineAll.Add(Error);
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
        public OutputData()
        {
            LineError = new List<string>();
            LineOut = new List<string>();
            LineAll = new List<string>();
            Out = new StringBuilder();
            Error = new StringBuilder();
            All = new StringBuilder();
        }
        public static OutputData operator +(OutputData left, OutputData right) {
            left.LineAll.AddRange(right.LineAll);
            left.LineOut.AddRange(right.LineOut);
            left.LineError.AddRange(right.LineError);
            left.All.Append(right.ToString());
            left.Out.Append(right.Out.ToString());
            left.Error.Append(right.Error.ToString());
            return left;
        }
        public override string ToString()
        {
            return All.ToString();
        }
    }
}
