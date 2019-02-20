using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Net.Getters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutumnBox.GUI.Model
{
    [JsonObject(MemberSerialization.OptOut)]
    class HomeSettings : ModelBase, JsonGetter<HomeSettings>.IJsonSettable
    {
        [JsonProperty("tips_enable")]
        public bool TipsEnable
        {
            get => _tips; set
            {
                _tips = value;
                RaisePropertyChanged();
            }
        }
        private bool _tips;

        [JsonProperty("cst_enable")]
        public bool CstEnable
        {
            get => _cst; set
            {
                _cst = value;
                RaisePropertyChanged();
            }
        }

        public string Json { get; set; }

        private bool _cst;
    }
}
