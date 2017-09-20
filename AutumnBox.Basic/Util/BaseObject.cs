namespace AutumnBox.Basic.Util
{
    using System;
    /// <summary>
    /// 基类
    /// </summary>
    public class BaseObject : Object
    {
        protected string TAG;
        public BaseObject() { this.TAG = this.GetType().Name; }
        protected void LogD(string message) { Logger.D(TAG, message); }
        protected void LogT(string message) { Logger.T(TAG, message); }
        protected void LogE(string message, Exception e, bool showInTrace = true)
        {
            Logger.E(TAG, message, e, true);
        }
    }
}
