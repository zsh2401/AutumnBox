using System;
using System.Reflection;
using System.Threading;
using AutumnBox.OpenFramework.Script;

public static void Main(ScriptArgs args)
{
    Console.WriteLine("xxx");
    Thread.Sleep(5000);
    Console.WriteLine(args);
}
public static string __Name()
{
    return "";
}
public static int __UpdateId()
{
    return -1;
}
public static string __ContactInfo()
{
    return "zsh2401@163.com";
}
public static string __Desc()
{
    return "Ok!";
}
public static Version __Version()
{
    return new Version("1.0.3");
}