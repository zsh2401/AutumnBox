/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/24 18:35:58 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.TestModV2
{
    public class GUITestMod : ExtendModule
    {
        public override string Name => "GUITestMod";

        protected override void OnStartCommand(StartArgs args)
        {
            var wind=  new AutumnBox.TestModV2.Window();
            
        }
    }
}
