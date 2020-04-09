using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Essentials.ExternalXCards
{
    class MotdXCard : IXCard
    {
        public int Priority => -100;

        public object View => "Motd";

        public void Create()
        {

        }

        public void Destory()
        {

        }

        public void Update()
        {

        }
    }
}
