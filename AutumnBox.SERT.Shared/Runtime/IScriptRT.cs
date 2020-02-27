using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.SERT.Runtime
{
    internal interface IScriptRT : IDisposable
    {
        string RTID { get; }
        void LoadEnv();
        void LoadScript(string code);

        event EventHandler<ScriptRTOutputEventArgs> LogReceived;

        object GetVar(string varName);
        T GetVar<T>(string varName);
        void SetVar(string varName, object value);

        bool VarAvailable(string name);

        Task<T> CallFuncAsync<T>(string funcName,params object[] args);
        Task<object> CallFuncAsync(string funcName, params object[] args);
        T CallFunc<T>(string funcName, params object[] args);
        object CallFunc(string funcName, params object[] args);
    }
    internal class ScriptRTOutputEventArgs : EventArgs
    {
        public ScriptRTOutputEventArgs(string content)
        {
            Content = content;
        }
        public string Content { get; }
    }
}
