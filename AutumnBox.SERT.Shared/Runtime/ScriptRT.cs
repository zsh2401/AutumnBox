using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutumnBox.SERT.Shared.Runtime
{
    internal class ScriptRT : IDisposable
    {
        private readonly V8ScriptEngine engine;

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

        public ScriptRT(string script)
        {
            engine = new V8ScriptEngine();
            InitEnv();
            engine.Execute(script);
        }

        public void ImportAsFunction(string functionName) { }

        public int Main()
        {
            try
            {
                if (engine.Invoke("main") is int retCode)
                    return retCode;
                else
                    return 0;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                return -1;
            }
        }

        public void Dispose()
        {
            engine?.Dispose();
        }

        private void InitEnv()
        {
            //engine.Execute("var atmb = {}");
            //engine.AddHostType("atmb.dn_Console", HostItemFlags.DirectAccess, typeof(Console));
            //engine.AddHostType("atmb.dn_Datetime", HostItemFlags.DirectAccess, typeof(DateTime));
            engine.AddHostType("___DNConsole", typeof(Console));
            engine.AddHostType("___DNDatetime", typeof(DateTime));

            engine.Execute(@"var atmb = {
                    dn_Console : ___DNConsole,
                    dn_DateTime : ___DNDatetime
            }");
            engine.AddHostObject("atmb.log", new Action<string>((string text) =>
            {
                Debug.WriteLine(text);
            }));
        }
    }
}
