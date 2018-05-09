/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/29 17:18:58 (UTC +8:00)
** desc： ...
*************************************************/
using Newtonsoft.Json;
using System;
namespace AutumnBox.GUI.PaidVersion
{
    [JsonObject(MemberSerialization.OptOut)]
    internal class Account : IAccount
    {
        [JsonProperty("isActivated")]
        public bool IsActivated { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string UserName { get; set; }
        [JsonProperty("edate")]
        public DateTime ExpiredDate { get; set; }
        [JsonProperty("rdate")]
        public DateTime RegisterDate { get; set; }
    }
}