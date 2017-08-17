using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Util
{
    internal class NoticeGetter
    {
        internal delegate void NoticeGetFinishHandler(Notice notice);
        internal event NoticeGetFinishHandler NoticeGetFinish;
        public void Get() {
            if (NoticeGetFinish == null) {
                throw new NotSetEventHandlerException("你不设置事件,那么这里获取完公告,怎么告诉你啊?:)");
            }
            Thread t = new Thread(_Check);
            t.Name = "Notice Check Thread";
            t.Start();
        }
        private void _Check() {
            NoticeGetFinish(Notice.GetNotice());
        }
    }
    
}
