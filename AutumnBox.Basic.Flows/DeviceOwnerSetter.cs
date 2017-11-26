/* =============================================================================*\
*
* Filename: IceActivator
* Description: 
*
* Version: 1.0
* Created: 2017/11/25 23:30:07 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.FlowFramework.Args;
using AutumnBox.Basic.Flows.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework.Container;
using AutumnBox.Basic.Flows.States;
using AutumnBox.Basic.FlowFramework.Events;
using AutumnBox.Support.CstmDebug;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 设备管理员设置器
    /// </summary>
    public abstract class DeviceOwnerSetter : FunctionFlow<FlowArgs, DeviceOwnerSetterResult>
    {
        /// <summary>
        /// 要设置DeviceOwner的包名
        /// </summary>
        protected abstract string PackageName { get; }
        /// <summary>
        /// 要设置DeviceOwner的包的类名
        /// </summary>
        protected abstract string ClassName { get; }
        protected virtual string Command => $"dpm set-device-owner {PackageName}/{ClassName}";
        protected virtual bool NeedReboot { get; } = true;
        protected ShellOutput _shellOutput;
        private ToolKit<FlowArgs> _toolKit;
        protected virtual bool TryFixDeviceAlreadyProvisioned()
        {
            Logger.T("<already provisioned error!> try to fix.....");
            var resultStep1 = _toolKit.Executer.QuicklyShell(_toolKit.Args.DevBasicInfo, "settings put global device_provisioned 0");
            var resultStep2 = _toolKit.Executer.QuicklyShell(_toolKit.Args.DevBasicInfo, Command);
            var resultStep3 = _toolKit.Executer.QuicklyShell(_toolKit.Args.DevBasicInfo, "settings put global device_provisioned 1");
            Logger.T("fix result ->" + (resultStep1.IsSuccess && resultStep2.IsSuccess && resultStep3.IsSuccess));
            Logger.T("fixing output ->\n" + resultStep1.OutputData.ToString());
            Logger.T(resultStep2.OutputData.ToString());
            Logger.T(resultStep3.OutputData.ToString());
            return resultStep1.IsSuccess && resultStep2.IsSuccess && resultStep3.IsSuccess;
        }
        protected override OutputData MainMethod(ToolKit<FlowArgs> toolKit)
        {
            Logger.D("the command ->" + Command);
            _toolKit = toolKit;
            _shellOutput = toolKit.Executer.QuicklyShell(toolKit.Args.DevBasicInfo, Command);
            return _shellOutput.OutputData;
        }
        [LogProperty(TAG = "DeviceOwner setter....Analyzing Result", Show = true)]
        protected override void AnalyzeResult(DeviceOwnerSetterResult result)
        {
            base.AnalyzeResult(result);
            result.ShellOutput = _shellOutput;
            result.OutputData = result.ShellOutput.OutputData;
            //如果结果输出中包含了success字样,则成功了,设置结果值并返回
            if (result.OutputData.Contains("success",true))
            {
                result.ErrorType = DeviceOwnerSetterErrType.None;
                result.ResultType = FlowFramework.States.ResultType.Successful;
                return;
            }
            result.ResultType = FlowFramework.States.ResultType.Unsuccessful;
            result.ErrorType = DeviceOwnerSetterErrType.Unknow;
            //用一堆该死的if/else if 判断输出来确定是哪一种错误
            if (result.OutputData.Contains("unknown admin"))
            {
                Logger.D("unknow admin");
                result.ErrorType = DeviceOwnerSetterErrType.UnknowAdmin;
                result.ResultType = FlowFramework.States.ResultType.Unsuccessful;
            }
            else if (result.OutputData.Contains("already set"))
            {
                result.ResultType = FlowFramework.States.ResultType.MaybeUnsuccessful;
                result.ErrorType = DeviceOwnerSetterErrType.DeviceOwnerIsAlreadySet;
            }
            else if (result.OutputData.Contains("device is already provisioned"))
            {
                result.ErrorType = DeviceOwnerSetterErrType.DeviceAlreadyProvisioned;
            }
            else if (result.OutputData.Contains("several accounts on the device"))
            {
                result.ErrorType = DeviceOwnerSetterErrType.ServalAccountsOnTheDevice;
            }
            else if (result.OutputData.Contains("several users on the device"))
            {
                result.ErrorType = DeviceOwnerSetterErrType.ServalUserOnTheDevice;
            }
            else if (result.OutputData.Contains("exception") && result.OutputData.Contains("java"))
            {
                result.ErrorType = DeviceOwnerSetterErrType.UnknowJavaException;
            }
            //如果是这个错误,就尝试修复
            if (result.ErrorType == DeviceOwnerSetterErrType.DeviceAlreadyProvisioned)
            {
                bool fixSuccess = TryFixDeviceAlreadyProvisioned();
                result.ErrorType = fixSuccess ? DeviceOwnerSetterErrType.None : result.ErrorType;
                result.ResultType = fixSuccess ? FlowFramework.States.ResultType.MaybeSuccessful : result.ResultType;
            }

        }
        protected override void OnFinished(FinishedEventArgs<DeviceOwnerSetterResult> e)
        {
            base.OnFinished(e);
            if (e.Result.ErrorType == DeviceOwnerSetterErrType.None && NeedReboot)
            {
                _toolKit.Ae("reboot");
            }
        }
    }
}
