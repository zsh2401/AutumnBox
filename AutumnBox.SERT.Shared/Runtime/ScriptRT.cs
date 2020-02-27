using AutumnBox.Logging;
using Microsoft.ClearScript.V8;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AutumnBox.SERT.Runtime
{
    internal class ScriptRT : IScriptRT
    {
        internal readonly V8ScriptEngine engine;

        public event EventHandler<ScriptRTOutputEventArgs> LogReceived;

        public bool HasMainFunction
        {
            get
            {
                try
                {
                    engine.Evaluate("main");
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public string RTID => throw new NotImplementedException();

        public ScriptRT(string script, ushort? port = null)
        {
            if (port != null)
            {
                engine = new V8ScriptEngine(V8ScriptEngineFlags.EnableDebugging | V8ScriptEngineFlags.AwaitDebuggerAndPauseOnStart, 9033);
            }
            else
            {
                engine = new V8ScriptEngine();
            }
            new EnvLoader(engine).Load();
            engine.Execute(script);
        }


        public int Main()
        {
            try
            {
                var result = engine.Invoke("main");
                Debug.WriteLine(result);
                if (result is int retCode)
                    return retCode;
                else
                    return 0;
            }
            catch (Exception exception)
            {
                SLogger<ScriptRT>.Warn(exception);
                return -1;
            }
        }

        public void Dispose()
        {
            engine?.Dispose();
        }

        public void LoadEnv()
        {
            throw new NotImplementedException();
        }

        public void LoadScript(string code)
        {
            throw new NotImplementedException();
        }

        object GetVar(string varName) { }
        T GetVar<T>(string varName) { }
        void SetVar(string varName, object value) { }

        public bool VarAvailable(string name)
        {
            throw new NotImplementedException();
        }

        Task<T> CallFuncAsync<T>(string funcName, params object[] args)
        {
            Action<object> onResolved = (resolve) => { };
            Action<object> onRejected = (reason) => { };
            dynamic promise = engine.Invoke(funcName, args);
            promise.then(onResolved, onRejected);
        }
        Task<object> CallFuncAsync(string funcName, params object[] args) { }
        T CallFunc<T>(string funcName, params object[] args) { }
        object CallFunc(string funcName, params object[] args) { }

        public Task<object> CallFuncAsync(string funcName, params string[] args)
        {
            throw new NotImplementedException();
        }

        public object CallFunc(string funcName, params string[] args)
        {
            throw new NotImplementedException();
        }

        public delegate void LogMethod(params object[] args);
    }
}
