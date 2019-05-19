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
        public static string Next()
        {
            try
            {
                int next = ran.Next(0, sentences.Length);
                return sentences[next];
            }
            catch
            {
                return "The best thing I could,is end it all";
            }
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
         "我知道已被忘却,流浪的航程太长太长,但那一刻要叫我一声啊,当人类又看到了蓝天,当鲜花又挂上了枝头,地球,我的流浪地球",
         "一壶浊酒尽余欢,今宵别梦寒",
         "长亭外 古道边,芳草碧连天",
         "记得，我们一起坐在海边看天,好像已过了千年万年",
         "东临碣石，以观沧海 水何澹澹，山岛竦峙\n树木丛生，百草丰茂 秋风萧瑟，洪波涌起",
         "今天，还不知道啊，你和思念哪个更远,明天，更不知道啊，我们要带着家园飞向谁边",
         "世界之外有一个等待,倒数的爱黑夜慢慢散开",
         "像钻石珍贵的希望,在心底在蔓延在绽放",
         "月光把天空照亮,洒下一片光芒点缀海洋",
         "每当流星从天而降,心中的梦想都随风飘扬",
         "展开透明翅膀越出天窗,找寻一个最美丽的希望",
         "不经历风雨 怎能有传奇,我会在这里等着你",
         "Now I'm here, blinking in the starlight,Now I'm here, suddenly I see"
        };
    }
}
