using AutumnBox.ConsoleTester.ObjTest;
using AutumnBox.Shared.CstmDebug;
using System;

namespace AutumnBox.ConsoleTester
{
    //[LogProperty(TAG = "Public")]
    class Program
    {
        //[LogProperty(TAG = "DDF")]
        static void Main(string[] args)
        {
            Logger.D("fuck!!!");
            MethodTest.StackTest.A();
            //Console.WriteLine(GUI.I18N.LanguageHelper.DefaultLanguage["LanguageName"].ToString());
            //new Config
            //var p = new Program();
            ////var full = new TestModuleTest().GetType().Namespace;
            //Logger.D(p,"fuck");
            //Thread.Sleep(1000);
            //Logger.D(p, "fuck");
            //Thread.Sleep(1000);
            //Logger.D(p, "fuck");
            //Thread.Sleep(1000);
            //Logger.D(p, "fuck");
            //Thread.Sleep(1000);
            //Logger.D(p, "fuck");
            ////Console.WriteLine(nameof(AutumnBox.Basic));
            //JsonPropTest.Run();
            Console.ReadKey();
        }
    }
}
