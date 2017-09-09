using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Util
{
    public class BaseObject
    {
        protected string TAG;
        public BaseObject() {
            this.TAG = this.GetType().Name;
            //try
            //{
            //    var d = this.ToString().Split('.');
            //    TAG = d[d.Length - 1];
            //}
            //catch (Exception e)
            //{
            //    Logger.E(TAG, "Init TAG fail", e);
            //}
        }
        protected void LogD(string message) {
            Logger.D(TAG, message);
        }
        protected void LogT(string message) {
            Logger.T(TAG, message);
        }
    }
}
