/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 19:14:15 (UTC +8:00)
** desc： ...
*************************************************/
using System.Collections.Generic;
using AutumnBox.OpenFramework.Entrance;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.ExampleExtensions
{
    public class Entrance : AtmbEntrance
    {
        public static Entrance Instance { get; private set; }
        public override string Name => "Example Entrance";

        public override int MinSdk => 8;

        public override int TargetSdk => 8;
        private List<IExtensionWarpper> exts;
        public Entrance()
        {
            Instance = this;
            exts = new List<IExtensionWarpper>(){
                GetWarpperFor(typeof(GuiExampleExtensions)),
                GetWarpperFor(typeof(FuckExt)),
            };
        }

        public void RemoveOne() {
            exts.RemoveAt(1);
        }
        public override IEnumerable<IExtensionWarpper> GetWarppers()
        {
            return exts;
        }
    }
}
