/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/31 23:33:19 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Service;

namespace AutumnBox.OpenFramework.Open.ServiceImpl
{
    [ServiceName(ServicesNames.SOUND)]
    internal class SSoundManager : AtmbService,ISoundService
    {
        /// <summary>
        /// 播放OK音效
        /// </summary>
        public void OK()
        {
            CallingBus.BaseApi.PlayOk();
        }
    }
}
