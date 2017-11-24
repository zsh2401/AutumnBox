/* =============================================================================*\
*
* Filename: FunctionFlow
* Description: 
*
* Version: 1.0
* Created: 2017/11/23 14:52:17 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework.Container;
using AutumnBox.Basic.FlowFramework.Events;
using AutumnBox.Basic.FlowFramework.States;
using AutumnBox.Support.CstmDebug;
using System;

namespace AutumnBox.Basic.FlowFramework
{
    partial class FunctionFlow<TArgs, TResult>
    {
        private CExecuter _executer;
        private int? _pid;
        private TResult _resultTmp;
        private bool isForceStoped = false;
        private void MainFlow()
        {
            try
            {
                _MainFlow();
            }
            catch (Exception e)
            {
                Logger.T("A exception happend on MainFlow()... ->" + e);
                try
                {
                    OnFinished(new FinishedEventArgs<TResult>(new TResult() { ResultType = ResultType.Exception, Exception = e }));
                }
                catch (Exception e_)
                {
                    Logger.T("Exception happend on OnFinished() when already happend a expcetion and reporting.....fuck!", e_);
                }
            }
        }
        private void _MainFlow()
        {
            /*Checking*/
            Logger.T("Checking...");
            TResult result = new TResult
            {
                CheckResult = Check()
            };
            Logger.T("Check Result ->" + result.CheckResult);
            if (result.CheckResult != CheckResult.OK)
            {
                OnFinished(new FinishedEventArgs<TResult>(result));
            }
            Logger.T("Startup...");
            OnStartup(new StartupEventArgs());
            Logger.T("Running...");
            result.OutputData = MainMethod(new ToolKit<TArgs>(Args, _executer));
            Logger.T("Analyzing...");
            AnalyzeResult(result);
            Logger.T("Finished...");
            OnFinished(new FinishedEventArgs<TResult>(result));
        }
    }
}
