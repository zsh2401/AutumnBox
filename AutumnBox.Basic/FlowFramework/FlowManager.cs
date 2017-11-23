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
using AutumnBox.Basic.FlowFramework.Args;
using AutumnBox.Basic.FlowFramework.Container;
using AutumnBox.Basic.FlowFramework.Flows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.FlowFramework
{
    public class FlowManager
    {
        private FunctionFlow<FlowArgs, Result> _flow;
        public FlowManager()
        {
            //_flow = new FLOW_T();
            //_flow.Create();
        }
        //public async void RunAsync()
        //{
        //   var  _flow = TestingFlows.Get(new TestingArgs());
            
        //}
        //public static void Testing()
        //{
        //    TestingFlows.Get();
        //}
        public void Run()
        {
            _flow.OnStartup(new Events.StartupEventArgs());
        }
    }
}
