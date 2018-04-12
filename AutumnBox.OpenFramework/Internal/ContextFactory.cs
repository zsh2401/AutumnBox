namespace AutumnBox.OpenFramework.Internal
{
    /// <summary>
    /// 上下文工厂,请勿调用
    /// </summary>
    public static class ContextFactory
    {
        /// <summary>
        /// 最高权限上下文
        /// </summary>
        private class HighPermissionContext : Context
        {
            public override string Tag => SourceContext.Tag;
            public Context SourceContext { get; private set; }
            public HighPermissionContext(Context lowPermissionContext)
            {
                SourceContext = lowPermissionContext;
            }
            internal override ContextPermissionLevel GetPermissionLevel()
            {
                return ContextPermissionLevel.High;
            }
        }
        /// <summary>
        /// 获取最高权限Context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Context GetHighPermissionContext(Context context)
        {
            context.PermissionCheck(ContextPermissionLevel.Mid);
            return new HighPermissionContext(context);
        }
    }
}
