/*

* ==============================================================================
*
* Filename: Program
* Description: 
*
* Version: 1.0
* Created: 2020/5/21 20:00:50
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.GUI.Services.Impl.OS;
using CommandLine;
using CommandLine.Text;
using System;
using System.Linq;

namespace AutumnBox.GUI
{
    class Program
    {
        public static Options CliOptions { get; private set; }

        [STAThread]
        static void Main(string[] args)
        {
            var parser = new Parser(with => with.HelpWriter = null);
            var parserResult = parser.ParseArguments<Options>(args);
            parserResult
              .WithParsed<Options>(options => Run(options))
              .WithNotParsed(errs => DisplayHelp(parserResult));
        }

        static void DisplayHelp<T>(ParserResult<T> result)
        {
            var helpText = HelpText.AutoBuild(result, h =>
            {
                h.AdditionalNewLineAfterOption = false;
                h.Heading = "AutumnBox"; //change header
                h.Copyright = "Copyright (c) 2020 www.atmb.top"; //change copyright text
                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, e => e);
            Console.WriteLine(helpText);
        }
        private static void Run(Options options)
        {
            CliOptions = options;
            if (options.Wait == false)
            {
                var process = OtherProcessChecker.ThereIsOtherAutumnBoxProcess();
                if (process != null)
                {
                    NativeMethods.SetForegroundWindow(process.MainWindowHandle);
                    Environment.Exit(0);
                    process.WaitForExit();
                }
            }
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
    public sealed class Options
    {
        [Option('w', "wait", Required = false, Default = false, HelpText = "Waiting for existed AutumnBox process exit.")]
        public bool Wait { get; set; } = false;

        [Option('g', "global-notification", Required = false, Default = false, HelpText = "Enable global notification.")]
        public bool GlobalNotification { get; set; }

        [Option("device-reporter", Required = false, Default = true, HelpText = "Enable device reporter.")]
        public bool DeviceReport { get; set; }

    }
}
