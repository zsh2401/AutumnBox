/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 17:58:21 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using System;

namespace AutumnBox.Basic.Flows
{
    public class ScreenShoterArgs:FlowArgs{
        public string SavePath { get; set; } = "";
    }
    public class ScreenShoter : FunctionFlow<ScreenShoterArgs>
    {
        protected override Output MainMethod(ToolKit<ScreenShoterArgs> toolKit)
        {
            string tempFileName = $"{DateTime.Now.ToString("yyyy_MM_dd_hh_MM_ss")}.png";
            var outputBuilder = new OutputBuilder();
            outputBuilder.Register(toolKit.Executer);
            toolKit.Ae($"shell /system/bin/screencap -p /sdcard/{tempFileName}");
            toolKit.Ae($"pull /sdcard/{tempFileName} {toolKit.Args.SavePath}");
            toolKit.Ae($"shell rm -rf /sdcard/{tempFileName}");
            return outputBuilder.Result;
        }
    }
}
