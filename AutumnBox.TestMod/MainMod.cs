using AutumnBox.OpenFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.TestMod
{
    public class MainMod : AutumnBoxMod
    {
        public override string Name => "测试模块";
        public override string Desc => "用于测试";
        public override Version Version => new Version("0.0.1");
        public override DateTime DevelopmentDate => new DateTime(2018, 2, 23);

        protected override void OnInit(InitArgs args)
        {
            base.OnInit(args);
            Log("Inited...");
        }
        protected override void OnStartCommand(StartArgs args) { }
    }
}
