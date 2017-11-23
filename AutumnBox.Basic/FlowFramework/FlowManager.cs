/* =============================================================================*\
*
* Filename: FlowManager
* Description: 
*
* Version: 1.0
* Created: 2017/11/23 15:24:00 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework.Args;
using AutumnBox.Basic.FlowFramework.Container;
using AutumnBox.Basic.FlowFramework.States;
using System;

namespace AutumnBox.Basic.FlowFramework
{
    public class FlowManager
    {
        public event OutputReceivedEventHandler OutputReceived;
        private FunctionFlow _flow;
        private FlowArgs _args;
        private FlowResult result;
        public async void RunAsync() { }
        public void Run() { }
        private void InitFlow()
        {
            result = new FlowResult();
            try
            {
                _flow.Create(_args);
                result.CheckResult = _flow.Check();
            }
            catch (Exception e)
            {
                result.CheckResult = CheckResult.Error;
            }
        }
        private void FlowStart() {
            if (result.CheckResult == CheckResult.OK) {
                _flow.OnStartup(new Events.StartupEventArgs());
                result.Output =  _flow.MainMethod(new ToolKit(_args,new CExecuter()));
            }
        }
        public static FlowManager Create(Type flowType, FlowArgs args)
        {
            return new FlowManager() { _flow = Activator.CreateInstance<FunctionFlow>(), _args = args };
        }
    }
    //private FunctionFlow<FlowArgs, FlowResult> _flow;
    //public async void RunAsync()
    //{
    //   var  _flow = TestingFlows.Get(new TestingArgs());

    //}
    //public static void Testing()
    //{
    //    TestingFlows.Get();
    //}
}
