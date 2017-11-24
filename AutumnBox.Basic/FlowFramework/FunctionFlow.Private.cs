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
                Logger.T("a exception happend on mainflow... ->" + e);
                OnFinished(new FinishedEventArgs<TResult>(new TResult() { ResultType = ResultType.Exception, Exception = e }));
            }
        }
        private void _MainFlow()
        {
            /*Checking*/
            TResult result = new TResult
            {
                CheckResult = Check()
            };
            if (result.CheckResult != CheckResult.OK)
            {
                OnFinished(new FinishedEventArgs<TResult>(result));
            }
            /*Startup*/
            OnStartup(new StartupEventArgs());
            /*Running*/
            result.OutputData = MainMethod(new ToolKit<TArgs>(Args, _executer));
            /*Analying*/
            AnalyzeResult(result);
            /*完成鸟~*/
            OnFinished(new FinishedEventArgs<TResult>(result));
        }
    }
}
