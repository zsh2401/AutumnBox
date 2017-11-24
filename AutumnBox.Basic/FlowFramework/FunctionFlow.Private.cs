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
using AutumnBox.Support.CstmDebug;
using System;

namespace AutumnBox.Basic.FlowFramework
{
    partial class FunctionFlow<ARGS_T, RESUL_T>
    {
        private CExecuter _executer = new CExecuter();
        private int? _pid;
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
            }
        }
        private void _MainFlow()
        {
            /*Checking*/
            RESUL_T result = new RESUL_T
            {
                CheckResult = Check()
            };
            if (result.CheckResult != CheckResult.OK)
            {
                OnFinished(new FinishedEventArgs<RESUL_T>(result));
            }
            /*Startup*/
            OnStartup(new StartupEventArgs());
            /*Running*/
            result.Output = MainMethod(new ToolKit<ARGS_T>(Args, _executer));
            /*Analying*/
            AnalyzeResuslt(result);
            /*完成鸟~*/
            OnFinished(new FinishedEventArgs<RESUL_T>(result));
        }
    }
}
