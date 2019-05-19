using AutumnBox.GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Net.Getters
{
    class HomeSettingsGetter : JsonGetter<HomeSettings>
    {
#if DEBUG
        public override string Url => "http://localhost:24010/_api_/home_v1/settings.json";
#else
         public override string Url => App.Current.Resources["WebApiHomeSettings"].ToString();
#endif
    }
}
