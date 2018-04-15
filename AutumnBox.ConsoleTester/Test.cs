using System;
using AutumnBox.OpenFramework.Script;
using AutumnBox.OpenFramework.Open;
using AutumnBox.Basic.Device;

/*
这个方法一定要写,相当于标准拓展中的OnStartCommand()函数
args.DeviceInfo是目标设备信息
 */
public static void Main(ScriptArgs args)
{
    OpenApi.Gui.ShowMessageBox(args.Context, "Hello", "Hello world!");
}
/*
相当于标准拓展中的InitAndCheck(),可写可不写
args.DeviceInfo是目标设备信息
 */
public static bool InitAndCheck(ScriptInitArgs args)
{
    return true;
}
/*
/*
相当于标准拓展中的OnStopCommand(),可写可不写
当用户主动要求停止时调用,如果你无法真正终止Main方法,
请返回false
 */
public static bool OnStop(ScriptStopArgs args)
{
    return true;
}
/*相当于标准拓展中的OnDestory(),可写可不写
在这个函数内,你必须释放所有本脚本使用的资源! */
public static void SDestory(ScriptDestoryArgs args)
{

}


//秋之盒将通过此方法获取脚本运行所需的名称,可写可不写
public static string __Name()
{
    return "测试脚本拓展";
}
//秋之盒将通过此方法获取脚本运行所需的设备状态,可写可不写
public static DeviceState __ReqState()
{
    return DeviceState.Poweron;
}
//秋之盒将通过此方法获取脚本所有者,可写可不写
public static string __Auth()
{
    return "zsh2401";
}
//秋之盒将通过此方法获取脚本所有者联系信息,可写可不写
public static string __ContactInfo()
{
    return "zsh2401@163.com";
}
//秋之盒将通过此方法获取脚本说明信息,可写可不写
public static string __Desc()
{
    return "Ok!";
}
//秋之盒将通过此方法获取脚本版本号,可写可不写
public static Version __Version()
{
    return new Version("1.0.3");
}