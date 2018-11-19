/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/20 18:34:34 (UTC +8:00)
** desc： ...
*************************************************/
using System.Media;

namespace AutumnBox.GUI.Util.Effect
{
    static class Sounds
    {
        public readonly static SoundPlayer OK;
        static Sounds()
        {
            OK = new SoundPlayer("Resources/Sound/ok.wav");
        }
    }
}
