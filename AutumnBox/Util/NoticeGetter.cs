#region About
/*NoticeGetter是一个公告获取器
   实例化之后,绑定一个公告获取完成的事件
   使用Get方法会新建一个线程获取公告
   获取完成后将会发生公告完成事件,并且会向事件处理函数传入公告信息
 */
#endregion
#define TEST_LOCAL_API
using AutumnBox.Debug;
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
#if !DEBUG
    internal class NoticeGetter
#else
    public class NoticeGetter
#endif
    {
        public delegate void NoticeGetFinishHandler(Notice notice);
        public event NoticeGetFinishHandler NoticeGetFinish;
        private const string DEFAULT_NOTICE_JSON = "{ \"content\":\"...\", \"version\":1 }";
        private const string TAG = "Notice Getter";
        private Guider guider;
        public NoticeGetter()
        {
            //guider = new Guider();
        }
        /// <summary>
        /// 开始获取公告
        /// </summary>
        public void Get()
        {
            if (NoticeGetFinish == null)
            {
#if! DEBUG
                throw new NotSetEventHandlerException("你不设置事件,那么这里获取完公告,怎么告诉你啊?:)");
#endif
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
            guider = new Guider();
            try
            {
                NoticeGetFinish(GetNotice());
            }
            catch(Exception e) {
                Log.d(TAG,"Get notice fail...");
                Log.d(TAG,e.Message);
            }
        }
        /// <summary>
        /// 获取公告
        /// </summary>
        /// <returns>公告数据结构</returns>
        private Notice GetNotice()
        {
            JObject d = GetSourceData();
            return new Notice
            {
                content = d["content"].ToString(),
                version = int.Parse(d["version"].ToString()),
                sourceData = d
            };
        }
        /// <summary>
        /// 获取原始的从网页爬下来的json数据
        /// </summary>
        /// <returns></returns>
        private JObject GetSourceData()
        {
            if (guider.isOk)
            {
                 return JObject.Parse(Tools.GetHtmlCode(guider["apis"]["daily_notice"].ToString()));
            }
            else
            {
                return JObject.Parse(DEFAULT_NOTICE_JSON);
            }
        }
    }

}
