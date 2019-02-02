using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.UI
{
    static class Sentences
    {
        private static readonly Random ran = new Random();
        public static string Value()
        {
            int next = ran.Next(0, sentences.Length);
            return sentences[next];
            //return sentences.Last();
        }
        private static readonly string[] sentences = new string[]
        {
            "When the streets are cold and lonely,And the cars they burn below me",
            "I know,I know I've let you down,I've been a fool to myself",
            "so with sadness in my heart, feel the best thing i could do,is end it all",
            "我们似乎走在了并列的时空,交错而过却永远不能重逢",
            "明明比任何人都更加接近,为何却始终无法互相感应",
            "真实地活在自己的世界里,是我最想要告诉你的话语",
            "夕阳下，曾经的约定，此刻的你，是否已忘记?",
            "I Love You, bliss, I love you on (and) on.",
            "I can see for miles and miles and miles and miles and miles",
            "In other words, please be true!In other words, I love you!",
            "what's done is done it feels so bad,what once was happy now is sad",
            "我所做的一切都已无法挽回,重温那些美好的回忆却只是感到难过",
            "I wish that I could turn back time,cos now the guilt is all mine",
            "与你交织紧握的手,传来了无与伦比的温暖,但愿永远不忘这份感觉,这是你所留下的温存",
            "Since I was young, I knew I’d find you,But our love was a song sung by a dying swan",
            "And in your dreams you’ll see me falling, falling,Breathe in the light,I’ll stay here in the shadow",
         "我知道已被忘却,流浪的航程太长太长,但那一刻要叫我一声啊,当人类又看到了蓝天,当鲜花又挂上了枝头,地球,我的流浪地球"
        };
    }
}
