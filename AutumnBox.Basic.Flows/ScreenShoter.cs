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
    /// <summary>
    /// 截图器参数
    /// </summary>
    public class ScreenShoterArgs:FlowArgs{
        /// <summary>
        /// 截图保存路径
        /// </summary>
        public string SavePath { get; set; } = "";
    }
    /// <summary>
    /// 截图器
    /// </summary>
    public class ScreenShoter : FunctionFlow<ScreenShoterArgs>
    {
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
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
