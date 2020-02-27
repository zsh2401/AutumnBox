using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using System;
using System.Diagnostics;
using System.Linq;

namespace AutumnBox.SERT.Runtime
{
    internal class EnvLoader
    {
        private readonly V8ScriptEngine engine;

        public EnvLoader(V8ScriptEngine engine)
        {
            if (engine is null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.engine = engine;
        }
        public void Load()
        {
            engine.ExecuteCommand("var atmb = {}");
            engine.AddHostObject("afx",new HostTypeCollection("AutumnBox.Basic","AutumnBox.Logging"));
            engine.ExecuteCommand("var basic = afx.AutumnBox.Basic");
            AddHostObjectToATMB("logf", new LogFormat((args) =>
            {
                if (args.Length > 1)
                {
                    Debug.WriteLine(args.Length);
                    Debug.WriteLine(args[0].ToString(), args.Skip(1).ToArray());
                }
                else
                {
                    Debug.WriteLine(args[0]);
                }
            }));
            AddHostTypeToATMB("DNConsole", typeof(Console));
            AddHostObjectToATMB("DNNow", DateTime.Now);
        }
        private void AddHostTypeToATMB(string name, Type type)
        {
            string bindingName = $"___atmb_binding__{name}";
            engine.AddHostType(bindingName, type);
            engine.ExecuteCommand($"atmb.{name} = {bindingName}");
        }
        private void AddHostObjectToATMB(string name, object obj)
        {
            string bindingName = $"___atmb_binding__{name}";
            engine.AddHostObject(bindingName, obj);
            engine.ExecuteCommand($"atmb.{name} = {bindingName}");
        }
        private void LoadDebugAPI() { }
        private delegate void LogFormat(params string[] args);
    }
}
