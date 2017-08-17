using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Util
{
    /// <summary>
    /// 公告获取器,获取完成后会发生NoticeGetFinish事件
    /// </summary>
    internal class NoticeGetter
    {
        internal delegate void NoticeGetFinishHandler(Notice notice);
        internal event NoticeGetFinishHandler NoticeGetFinish;
        private static string noticeApiUrl = "https://raw.githubusercontent.com/zsh2401/AutumnBox/master/Api/gg.json";
        /// <summary>
        /// 开始获取公告
        /// </summary>
        public void Get()
        {
            if (NoticeGetFinish == null)
            {
                throw new NotSetEventHandlerException("你不设置事件,那么这里获取完公告,怎么告诉你啊?:)");
            }
            Thread t = new Thread(_Get);
            t.Name = "Notice Check Thread";
            t.Start();
        }
        /// <summary>
        /// 具体的获取逻辑
        /// </summary>
        private void _Get()
        {
            NoticeGetFinish(GetNotice());
        }
        /// <summary>
        /// 获取公告
        /// </summary>
        /// <returns>公告数据结构</returns>
        public static Notice GetNotice()
        {
            JObject d = GetSourceData();
            return new Notice {
                content = d["content"].ToString(),
                version = int.Parse(d["version"].ToString()),
                sourceData = d
            };
        }
        /// <summary>
        /// 获取原始的从网页爬下来的json数据
        /// </summary>
        /// <returns></returns>
        private static JObject GetSourceData()
        {
#if TEST_LOCAL_API//当处于DEBUG编译时,这个编译符也会被定义
            return JObject.Parse(File.ReadAllText("../Api/gg.json"));
#else
            return JObject.Parse(Tools.GetHtmlCode(noticeApiUrl));
#endif
        }
    }

}
