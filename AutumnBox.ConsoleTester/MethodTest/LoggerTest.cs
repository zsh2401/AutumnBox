/* =============================================================================*\
*
* Filename: LoggerTest
* Description: 
*
* Version: 1.0
* Created: 2017/10/31 3:22:34 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.ConsoleTester.MethodTest
{
    public class Dispatcher
    {
        public void Invoke(Action action)
        {
            action();
        }
    }
    //[LogProperty(TAG = "Testing...", Show = true)]
    public class LoggerTest : ILogSender
    {
        LogSender sender;
        Dispatcher fuck = new Dispatcher();
        public string LogTag { get; } = "Fuck";
        public void LogSenderTest()
        {
            Logger.D(new LogSender(this),"GG");
        }
        public bool IsShowLog { get; } = true;

        public LoggerTest()
        {
            sender = new LogSender(this);
        }
        [LogProperty(TAG = "FUCK")]
        public void InternalClassTest()
        {
            Logger.D("Start");
            new Thread(() =>
            {
                Console.WriteLine(new StackTrace().GetFrames()[3].GetMethod().Name);
                fuck.Invoke(() =>
                {
                    Logger.D(sender, "fuck");
                });
            }).Start();
        }
        public void ILogSenderTest()
        {
            Logger.D(this, "HEHEHE");
        }
        public static void Test()
        {
            Logger.D("Wow!");
        }
        [LogProperty(Show = true)]
        public static void ExpTest()
        {
            Logger.D("Wtf!", true);
            Logger.D("hehe", new FileNotFoundException("GG"));
        }
        [LogProperty(TAG = "Speed Test")]
        public static void SpeedTest(int maxValue = 1000)
        {
            DateTime startTime = DateTime.Now;
            for (int i = 0; i < maxValue; i++)
            {
                Logger.D(i.ToString());
            }
            DateTime finishTime = DateTime.Now;
            TimeSpan useTime = finishTime - startTime;
            Console.WriteLine("use " + useTime.TotalMilliseconds + $"ms by {maxValue} log print");
            Console.WriteLine("average " + Math.Round((useTime.TotalMilliseconds / maxValue), 3).ToString() + " ms");
        }
        public static void LambdaTest()
        {
            Action a = () =>
            {
                Action b = () =>
                {
                    Logger.D("Hehe");
                };
                b();
            };
            a();
        }
    }
}
